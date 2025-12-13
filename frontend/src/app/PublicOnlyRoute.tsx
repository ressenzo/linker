import {
    Navigate
} from "react-router-dom";

import { useAuth } from "./AuthContext";
import type { JSX } from "react";

export function PublicOnlyRoute({ children }: { children: JSX.Element }) {
    const { isAuthenticated } = useAuth();

    if (isAuthenticated) {
        return <Navigate to="/dashboard" replace />;
    }

    return children
}
