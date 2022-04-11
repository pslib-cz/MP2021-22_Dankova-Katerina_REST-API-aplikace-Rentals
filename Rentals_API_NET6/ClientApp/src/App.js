import { Router, Route, Switch, Link, NavLink } from "react-router-dom";
import "./App.css";
import Content from "./components/Content/Content";
import Footer from "./components/Footer/Footer";
import { createBrowserHistory } from "history";
import ScrollToTop from "./ScrollToTop";
import { WavyContainer } from "react-wavy-transitions";
import SignInCallback from "./components/auth/SignInCallback";
import SilentRenew from "./components/auth/SilentRenew";
import SignOutCallback from "./components/auth/SignOutCallback";
import { Proomkabar } from "proomkatest";
import { useAppContext } from "./providers/ApplicationProvider";
import { useEffect, useState } from "react";

const history = createBrowserHistory();

function App() {
  const [{ userManager, accessToken, profile }] = useAppContext();
  const w = window.innerWidth;
  const [admin, setAdmin] = useState(false);

  useEffect(() => {
    if (profile?.rentals_admin) {
      setAdmin(true);
    }
  }, [profile]);

  return (
    <>
      <WavyContainer className="indexed" />

      <Router history={history}>
        <ScrollToTop>
          <div id="scroll">
            <Proomkabar className="indexed">
              {accessToken ? (
                <div
                  style={{
                    display: "flex",
                    flexDirection: "row",
                    fontSize: "24px",
                    cursor: "pointer",
                  }}
                  onClick={() => {
                    history.push("/account");
                  }}
                >
                  <img
                    src="./per.png"
                    id="myImg"
                    alt="me"
                    className="login"
                    onError={(e) => {
                      e.currentTarget.src = "per.png";
                    }}
                  ></img>
                  <div
                    style={{
                      display: "grid",
                      placeItems: "center",
                      width: "100%",
                    }}
                  >
                    <p
                      style={{
                        maxWidth: "100%",
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        whiteSpace: "nowrap",
                      }}
                    >
                      {profile?.name}
                    </p>
                  </div>
                </div>
              ) : (
                <i
                  className="fas fa-user login"
                  onClick={() => {
                    userManager.signinRedirect();
                  }}
                ></i>
              )}

              <NavLink
                className="navigation-item"
                tag={Link}
                to={"/"}
                exact={true}
                activeClassName={"active"}
              >
                {w < 1280 ? (
                  <i className="fas fa-home"></i>
                ) : (
                  <>
                    <i className="fas fa-home"></i>
                    <p>Domů</p>
                  </>
                )}
              </NavLink>

              <NavLink
                className="navigation-item"
                tag={Link}
                to={"/favorite"}
                activeClassName={"active"}
              >
                {w < 1280 ? (
                  <i className="fas fa-heart"></i>
                ) : (
                  <>
                    <i className="fas fa-heart"></i>
                    <p>Oblíbené</p>
                  </>
                )}
              </NavLink>
              <NavLink
                className="navigation-item"
                tag={Link}
                to={"/account"}
                activeClassName={"active"}
              >
                {w < 1280 ? (
                  <i className="fas fa-user"></i>
                ) : (
                  <>
                    <i className="fas fa-user"></i>
                    <p>Profil</p>
                  </>
                )}
              </NavLink>
              <NavLink
                className="navigation-item"
                tag={Link}
                to={"/bag"}
                activeClassName={"active"}
              >
                {w < 1280 ? (
                  <i className="fas fa-shopping-bag"></i>
                ) : (
                  <>
                    <i className="fas fa-shopping-bag"></i>
                    <p>Košík</p>
                  </>
                )}
              </NavLink>
              {admin ? (
                <NavLink
                  className="navigation-item"
                  tag={Link}
                  to={"/admin"}
                  activeClassName={"active"}
                >
                  {w < 1280 ? (
                    <i className="fas fa-cog"></i>
                  ) : (
                    <>
                      <i className="fas fa-cog"></i>
                      <p>Admin</p>
                    </>
                  )}
                </NavLink>
              ) : null}
            </Proomkabar>
            <Content />
          </div>

          <Footer />
        </ScrollToTop>
        <Switch>
          <Route path="/oidc-callback" component={SignInCallback} />
          <Route path="/oidc-signout-callback" component={SignOutCallback} />
          <Route path="/oidc-silent-renew" component={SilentRenew} />
          <Route path="/sign-in" />
          <Route path="/sign-out" />
        </Switch>
      </Router>
    </>
  );
}

// Force reload when mobile
// orientation changes
window.screen.orientation.addEventListener("change", function (e) {
  window.location.reload(false);
});
export default App;
