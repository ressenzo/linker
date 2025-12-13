import {
    createBrowserRouter
} from "react-router-dom"

import {
    MAIN_ROUTE
} from "./constants"

import { Login } from "../pages/Login/Login"
import { ProtectedRoute } from "./ProtectedRoute"
import { PublicOnlyRoute } from "./PublicOnlyRoute";
import { Wrapper } from "../pages/Wrapper/Wrapper";
import { Dashboard } from "../pages/Dashboard";

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
        path: MAIN_ROUTE,
        element: (
            <ProtectedRoute>
                <Wrapper />
            </ProtectedRoute>
        ),
        children: [
            {
                path: "dashboard",
                element: <Dashboard />
            }
        ]
    },
]);
