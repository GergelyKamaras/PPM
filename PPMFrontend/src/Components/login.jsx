import AuthFetch from "../DataCommunication/AuthFetch";
import { loginEndpoint } from "../Config";
import { useAuth } from "../Contexts/AuthContext";

export default function Login()
{
    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

    function handleSubmit(e){
        e.preventDefault();
        let form = new FormData(document.querySelector('#loginForm'));
        AuthFetch(loginEndpoint, form, true, setIsLoggedIn, setAuthUser);
    }

    return (
        <>
            <h1>Login</h1>
            <form id="loginForm">
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input id="email" name="email" className="form-control"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="password">Password</label>
                    <input id="password" name="password" className="form-control"></input>
                </div>
                <div className="form-group">
                    <button className="btn btn-primary" onClick={handleSubmit}>Submit</button>
                </div>
            </form>
        </>
    )
}
