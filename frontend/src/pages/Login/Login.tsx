import "./Login.css";

import {
    useAuth
} from "../../app/AuthContext"

import {
    useNavigate
} from "react-router-dom"

import { Input } from "../../components/Input/Input";
import { useState } from "react";
import { Button } from "../../components/Button/Button";

export function Login() {
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleLogin = () => {
        login("fake-jwt-token");
        navigate("/dashboard");
    }

    const [userName, setUserName] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    return (
        <div className="d-flex align-items-center justify-content-center min-vh-100 bg-body-tertiary">
            <main className="form-signin w-100" style={{ maxWidth: '330px' }}>
                <form>
                    <h1 className="text-center h3 mb-3 fw-normal linker_login-title">Linker</h1>
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
                    <Button
                        id="login-sign-in"
                        onClick={handleLogin}
                        text="Sign in"
                        type="submit"
                        isFull={true}
                    />
                    {
                        JSON.stringify(userName)
                    }
                    {
                        JSON.stringify(password)
                    }
                </form>
            </main>
        </div>
    )
}
