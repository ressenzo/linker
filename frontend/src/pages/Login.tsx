import {
    useAuth
} from "../app/AuthContext"

import {
    useNavigate
} from "react-router-dom"

export function Login() {
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleLogin = () => {
        login("fake-jwt-token");
        navigate("/dashboard");
    }

    return (
        <div>
            <h1>Login</h1>
            <button onClick={handleLogin}>Entrar</button>
        </div>
    )
}
