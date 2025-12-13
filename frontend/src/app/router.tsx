import {
    createBrowserRouter
} from "react-router-dom"

import { Login } from "../pages/Login/Login"
import { Dashboard } from "../pages/Dashboard"
import { ProtectedRoute } from "./ProtectedRoute"
import { PublicOnlyRoute } from "./PublicOnlyRoute";

export const router = createBrowserRouter([
    {
        path: "/login",
        element: (
            <PublicOnlyRoute>
                <Login />
            </PublicOnlyRoute>
        ),
    },
    {
        path: "/dashboard",
        element: (
            <ProtectedRoute>
                <Dashboard />
            </ProtectedRoute>
        ),
    },
]);
