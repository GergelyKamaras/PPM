export default function AuthFetch(url, data, login = false)
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
                                alert("Succesfull login!");
                            });
                }
                alert("Succesfull registration!");
            }
            else
            {
                alert("Error During operation!");
            }
        })
}
