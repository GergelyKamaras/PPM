import registerUser from "../DataCommunication/RegisterUser.js";
import { registrationEndpoint } from "../Config.js";
export default function Registration()
{
    function handleSubmit(e){
        e.preventDefault();
        let form = new FormData(document.querySelector('#registrationForm'));
        registerUser(registrationEndpoint, form);
    }

    return (
        <>
            <h1>Registration</h1>
            <form id="registrationForm">
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input name="email" id="email" className="form-control"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="username">Username</label>
                    <input name="username" id="username" className="form-control"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="password">Password</label>
                    <input name="password" id="password" className="form-control"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="firstname">Firstname</label>
                    <input name="firstname" id="firstname" className="form-control"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="lastname">Lastname</label>
                    <input name="lastname" id="lastname" className="form-control"></input>
                </div>
                <div className="form-group">
                    <label htmlFor="role">Role</label>
                    <select name="role" id="role">
                        <option value="Administrator">Administrator</option>
                        <option value="Owner">Owner</option>
                        <option value="Tenant">Tenant</option>
                    </select>
                </div>
                <div className="form-group">
                    <button className="btn btn-primary" onClick={handleSubmit}>Submit</button>
                </div>
            </form>
        </>
    )
}
