export default function registerUser(url, data)
{
    let domain = "https://" + window.location.hostname;

    fetch(domain + url, {
        method: "POST",
        body: data})
        .then(response => {
            if (response.status === 200)
            {
                alert("Successfully registered user!");
            }
            else
            {
                alert("Error while registering user!");
            }
        })
}
