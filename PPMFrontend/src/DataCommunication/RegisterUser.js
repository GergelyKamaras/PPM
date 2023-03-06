export default function registerUser(url, data)
{
    fetch(url, {
        method: "POST",
        body: json.stringify(data)})
        .then(response => response.json())
        .then(result => {
            if (result.status === "Success")
            {
                alert("You have registered a user!");
            }
            else
            {
                alert("Error registering the user!");
            }
        })
}
