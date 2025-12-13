import {
    Navigate
} from "react-router-dom";

import {
    MAIN_ROUTE
} from "./constants"

import { useAuth } from "./AuthContext";
import type { JSX } from "react";

export function PublicOnlyRoute({ children }: { children: JSX.Element }) {
    const { isAuthenticated } = useAuth();

    if (isAuthenticated) {
        return <Navigate to={MAIN_ROUTE} replace />;
    }

    return children
}
