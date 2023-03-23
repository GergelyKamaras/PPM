export default async function SendData(method, url, payload)
{
    let domain = "https://" + window.location.hostname;

    let response = await fetch(domain + url, {
        method: method,
        headers: {
            "Authorization": localStorage.getItem("AccessToken"),
        },
        body: payload
    })

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
