import loginUser from "../DataCommunication/LoginUser";
import { loginEndpoint } from "../Config";

export default function Login()
{
    function handleSubmit(e){
        e.preventDefault();
        let form = new FormData(document.querySelector('#loginForm'));
        loginUser(loginEndpoint, form);
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
