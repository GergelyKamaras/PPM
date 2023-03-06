export default function Login()
{
    function handleSubmit(e){
        e.preventDefault();
        let form = document.querySelector('#loginForm');
        console.log(new FormData(form));
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
