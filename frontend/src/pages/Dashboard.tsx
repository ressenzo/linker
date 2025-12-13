import {
    useAuth
} from "../app/AuthContext"

export function Dashboard() {
    const { logout } = useAuth();

    return (
        <div>
            <h1>Dashboard</h1>
            <button onClick={logout}>Sair</button>
        </div>
    )
}
