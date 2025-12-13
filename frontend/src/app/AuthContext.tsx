import {
    createContext,
    useContext,
    useState
} from "react"

type AuthContextType = {
    isAuthenticated: boolean;
    login: (token: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType>(null!);

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const TOKEN_NAME: string = "linker-token";

    const [token, setToken] = useState<string | null>(
        localStorage.getItem(TOKEN_NAME)
    );

    const login = (token: string) => {
        localStorage.setItem(
            TOKEN_NAME,
            token
        );
        setToken(token)
    }

    const logout = () => {
        localStorage.removeItem(TOKEN_NAME);
        setToken(null)
    }

    return (
        <AuthContext.Provider
            value={{ isAuthenticated : !!token, login, logout }}
        >
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => useContext(AuthContext);
