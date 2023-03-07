export default async function FetchData(url)
{
    let domain = "https://" + window.location.hostname;

    let response = await fetch(domain + url, {
        method: "Get",
        headers: {
            "Authorization": localStorage.getItem("AccessToken"),
        }})

    if (response.status === 200)
    {
        let data = response.json();
        return data;
    }
    else
    {
        alert("Error During operation!");
    }
}
