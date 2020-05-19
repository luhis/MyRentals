import { FunctionalComponent, h, createContext } from "preact";
import { Route, Router } from "preact-router";
import { useGoogleLogin } from "react-use-googlelogin";
import { useContext, useState, StateUpdater } from "preact/hooks";

import Home from "../routes/home";
import NotFoundPage from "../routes/notfound";
import Header from "./header";
import Apartments from "../routes/apartments";
import Clients from "../routes/clients";
import Realtors from "../routes/realtors";

import "rbx/index.css";
import { GoogleAuth, Access } from "../types/models";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
if ((module as any).hot) {
    // tslint:disable-next-line:no-var-requires
    require("preact/debug");
}

const GoogleAuthContext = createContext<GoogleAuth>({
    signIn: () => {
        throw new Error();
    },
    googleUser: null,
}); // Not necessary, but recommended.

const AccessContext = createContext<[Access, StateUpdater<Access>]>([
    { canViewRealtors: false },
    (_) => undefined,
]);

const App: FunctionalComponent = () => {
    const googleAuth = useGoogleLogin({
        clientId: process.env.PREACT_APP_GOOGLE_CLIENT_ID as string, // Your clientID from Google.
    });
    const access = useState<Access>({ canViewRealtors: false });
    return (
        <div id="app">
            <AccessContext.Provider value={access}>
                <GoogleAuthContext.Provider value={googleAuth as GoogleAuth}>
                    <Header />
                    <Router>
                        <Route path="/" component={Home} />
                        <Route path="/apartments/" component={Apartments} />
                        <Route path="/clients/" component={Clients} />
                        <Route path="/realtors/" component={Realtors} />
                        <NotFoundPage default />
                    </Router>
                </GoogleAuthContext.Provider>
            </AccessContext.Provider>
        </div>
    );
};

export default App;

export const useGoogleAuth = () => useContext(GoogleAuthContext);
export const useAccess = () => useContext(AccessContext);
