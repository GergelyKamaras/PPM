import { useAuth } from "../Contexts/AuthContext";
import { financialObjectsEndpoint, propertiesEndpoint } from "../Config";
import { DataEntryModal } from "./DataEntryModal";

export default function NavBar() {
    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

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
                            <li className="nav-item">
                                <a className="nav-link" href="">My Properties</a>
                            </li>
                            <li>
                                <DataEntryModal type="Property" url={propertiesEndpoint}/>
                            </li>
                            <li>
                                <DataEntryModal type="FinancialObject" url={financialObjectsEndpoint}/>
                            </li>
                        </ul>
                    </nav>
                    <p>Welcome {authUser["Name"]}!</p>
                </div>
            );
        }

    }
