function NavBar() {

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <ul className="navbar-nav mr-auto">
                    <li className="nav-item active">
                        <a className="navbar-brand" href="/">PPM</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="/registration">Registration</a>
                    </li>
                    <li className="nav-item active">
                        <a className="nav-link" href="/login">Login</a>
                    </li>
                </ul>
            </nav>
        </div>
    )
  }
  export default NavBar
