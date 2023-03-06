export default function loginUser(url, data)
{
    let domain = "https://" + window.location.hostname;

    fetch(domain + url, {
        method: "POST",
        body: data})
        .then(response => {
            if (response.status === 200)
            {
                response.json()
                    .then(r => console.log(r));
                alert("You have logged in!");
            }
            else
            {
                alert("Error Logging in!");
            }
        })
}
