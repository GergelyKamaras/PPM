import FetchData from "./DataCommunication/FetchData"

function handleClick(e) {
    e.preventDefault();
    let url = e.target.dataset.url;
    FetchData(url).then(r => console.log(r));
}

function handleLogout(e)
{
    e.preventDefault();
    localStorage.removeItem("AccessToken");
}

export default function NavBar() {
    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <ul className="navbar-nav mr-auto">
                    <li className="nav-item">
                        <a className="nav-link" href="/">PPM</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="/registration">Registration</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="/login">Login</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="" onClick={handleLogout}>Logout</a>
                    </li>
                    <li>
                        <button id="authtest" onClick={handleClick} data-url=":8000/authtest">AuthTest</button>
                    </li>
                    <li>
                        <button id="admintest" onClick={handleClick} data-url=":8000/admintest">AdminTest</button>
                    </li>
                    <li>
                        <button id="test" onClick={handleClick} data-url=":8000">Test</button>
                    </li>
                </ul>
            </nav>
        </div>
    )
    }
