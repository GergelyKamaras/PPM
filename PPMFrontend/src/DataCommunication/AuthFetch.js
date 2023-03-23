import jwt_decode from "jwt-decode";
import { EMAIL_KEY, NAME_KEY, ROLE_KEY } from "../Config";

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
