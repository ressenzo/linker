import "./Login.css";

import {
    useAuth
} from "../../app/AuthContext"

import {
    useNavigate
} from "react-router-dom"

import {
    MAIN_ROUTE
} from "../../app/constants"

import { Input } from "../../components/Input/Input";
import { useState, type FormEvent } from "react";
import { Button } from "../../components/Button/Button";

export function Login() {
    const { login } = useAuth();
    const navigate = useNavigate();
    const [userName, setUserName] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [errorMessage, setErrorMessage] = useState<string>("");

    const handleLogin = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (userName == "test" && password == "test") {
            login("fake-jwt-token");
            navigate(MAIN_ROUTE);
            return;
        }

        setErrorMessage("Invalid credentials")
    }



    return (
        <div className="d-flex align-items-center justify-content-center min-vh-100 bg-body-tertiary">
            <main className="form-signin w-100" style={{ maxWidth: '330px' }}>
                <form onSubmit={handleLogin}>
                    <h1 className="text-center h1 mb-3 fw-normal linker_login-title">Linker</h1>
                    <Input
                        id="login-username"
                        label="Username"
                        placeholder="Username"
                        type="text"
                        setChange={setUserName}
                    />
                    <Input
                        id="login-password"
                        label="Password"
                        placeholder="Password"
                        type="password"
                        setChange={setPassword}
                    />
                    <div className="text-danger mb-3">{errorMessage}</div>
                    <Button
                        id="login-sign-in"
                        text="Sign in"
                        type="submit"
                        className={"btn btn-success py-2 w-100"}
                        disabled={userName == "" || password == ""}
                    />
                </form>
            </main>
        </div>
    )
}
