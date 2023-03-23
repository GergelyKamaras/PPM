import FetchData from "../DataCommunication/FetchData";
import { useAuth } from "../Contexts/AuthContext";

export default function NavBar() {
    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

        function handleClick(e) {
            e.preventDefault();
            let url = e.target.dataset.url;
            FetchData(url).then(r => console.log(r));
        }

        function handleLogout(e)
        {
            e.preventDefault();
            localStorage.removeItem("AccessToken");
            setIsLoggedIn(false);
            setAuthUser(null);
        }

        if (!isLoggedIn)
        {
            return (
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
                    </ul>
                </nav>
            );
        }
        else
        {
            return (
                <div>
                    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                        <ul className="navbar-nav mr-auto">
                            <li className="nav-item">
                                <a className="nav-link" href="/">PPM</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="" onClick={handleLogout}>Logout</a>
                            </li>
                        </ul>
                    </nav>
                    <p>Welcome {authUser["Name"]}!</p>
                </div>
            );
        }

    }
