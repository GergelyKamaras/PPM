import jwt_decode from "jwt-decode";
import { EMAIL_KEY, NAME_KEY, ROLE_KEY } from "../Config";
import React, {useState, useEffect, useContext} from "react";

const AuthContext = React.createContext();

export function useAuth() {
    return useContext(AuthContext);
}

export function AuthProvider(props) {
    const [authUser, setAuthUser] = useState(null);

    const [isLoggedIn, setIsLoggedIn] = useState(false);

    useEffect(() => {
        if (localStorage.getItem("AccessToken") === null)
        {
            setIsLoggedIn(false);
        }
        else
        {
            let token = localStorage.getItem("AccessToken").split(" ")[1];
            var decoded = jwt_decode(token);
            setAuthUser({
                Id: decoded["Id"],
                Name: decoded[NAME_KEY],
                Role : decoded[ROLE_KEY],
                Email: decoded[EMAIL_KEY]
            });
            setIsLoggedIn(true);
        }
    }, [])

    const value = {
        authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn
    }

    return (
        <AuthContext.Provider value={value}>{props.children}</AuthContext.Provider>
    )
}
