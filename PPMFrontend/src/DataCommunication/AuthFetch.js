import jwt_decode from "jwt-decode";
import { EMAIL_KEY, NAME_KEY, ROLE_KEY } from "../Config";

function decodeToken() {
    var token = localStorage.getItem("AccessToken");
    console.log(token);
    var decoded = jwt_decode(token);
    console.log(decoded);
    console.log(decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]);
    console.log(decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]);
    console.log(decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
    console.log(decoded["Id"]);

}

export default function AuthFetch(url, data, login = false, setIsLoggedIn, setAuthUser)
{
    let domain = "https://" + window.location.hostname;

    fetch(domain + url, {
        method: "POST",
        body: data})
        .then(response => {
            if (response.status === 200)
            {
                if (login)
                {
                    response.json()
                        .then(r =>
                            {
                                localStorage.setItem("AccessToken", `Bearer ${r.token}`);
                                var decoded = jwt_decode(r.token);
                                setAuthUser({
                                    Id: decoded["Id"],
                                    Name: decoded[NAME_KEY],
                                    Role : decoded[ROLE_KEY],
                                    Email: decoded[EMAIL_KEY]
                                })
                                setIsLoggedIn(true);
                            });
                }
                else
                {
                    alert("Succesfull registration!");
                }
            }
            else
            {
                alert("Error During operation!");
            }
        })
}
