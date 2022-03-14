import React, { createContext, useReducer, useContext, useEffect } from "react";
import { UserManager, WebStorageStateStore, Log } from "oidc-client";
import {
  IDENTITY_CONFIGURATION,
  METADATA_OIDC,
} from "../configuration/authorization";
import ReactDOM from "react-dom";
import { Alert, Card } from "proomkatest";
import axios from "axios";

export const SET_ACCESS_TOKEN = "SET_ACCESS_TOKEN";
export const CLEAR_ACCESS_TOKEN = "CLEAR_ACCESS_TOKEN";
export const SET_ID_TOKEN = "SET_ID_TOKEN";
export const CLEAR_ID_TOKEN = "CLEAR_ID_TOKEN";
export const USER_EXPIRED = "USER_EXPIRED";
export const USER_FOUND = "USER_FOUND";
export const USER_EXPIRING = "USER_EXPIRING";
export const LOADING_USER = "LOADING_USER";
export const SILENT_RENEW_ERROR = "SILENT_RENEW_ERROR";
export const SESSION_TERMINATED = "SESSION_TERMINATED";
export const LOAD_USER_ERROR = "LOAD_USER_ERROR";
export const USER_SIGNED_OUT = "USER_SIGNED_OUT";
export const SET_ICON = "SET_ICON";
export const UPDATE_PHOTO = "UPDATE_PHOTO";

const userStore = window.localStorage;
const userManager = new UserManager({
  ...IDENTITY_CONFIGURATION,
  userStore: new WebStorageStateStore({ store: userStore }),
  metadata: {
    ...METADATA_OIDC,
  },
});

const initialState = {
  userManager: userManager,
  accessToken: null,
  idToken: null,
  userId: null,
  profile: null,
  isUserLoading: false,
};
Log.logger = console;
Log.level = Log.ERROR;

const parseJwt = (token) => {
  const base64Url = token.split(".")[1];
  const base64 = base64Url.replace("-", "+").replace("_", "/");
  return JSON.parse(window.atob(base64));
};

const reducer = (state, action) => {
  switch (action.type) {
    case UPDATE_PHOTO:
      return { ...state };
    case LOADING_USER:
      return { ...state, isUserLoading: true };
    case SET_ACCESS_TOKEN:
      return { ...state, accessToken: action.payload };
    case CLEAR_ACCESS_TOKEN:
      return { ...state, accessToken: null };
    case SET_ID_TOKEN:
      return { ...state, idToken: action.payload };
    case CLEAR_ID_TOKEN:
      return { ...state, idToken: null };
    case USER_FOUND:
      axios
        .get("https://oauth.pslib.cloud/api/account/original", {
          responseType: "blob",
          headers: {
            Authorization: "Bearer " + action.accessToken,
          },
        })
        .then((resp) => {
          ReactDOM.render(
            <>
              <Alert
                textColor="white"
                width="16rem"
                height="4rem"
                color="#00ae7c"
                delay="7500"
              >
                <i className="far fa-check-circle icon" /> Úspěšně přihlášen
              </Alert>

              <Alert
                textColor="white"
                width="16rem"
                height="4rem"
                color="#007784"
                delay="5000"
              >
                <i className="far fa-check-circle icon" /> Fotka aktualizována
              </Alert>
            </>,
            document.getElementById("alerts")
          );
          const url = URL.createObjectURL(resp.data);
          console.log(url);
          document.getElementById("myImg").src = url;
        });
      ReactDOM.render(
        <div id="null"></div>,
        document.getElementById("loginpls")
      );
      return {
        ...state,
        idToken: action.idToken,
        accessToken: action.accessToken,
        userId: action.userId,
        profile: action.profile,
        isUserLoading: false,
      };

    case SET_ICON: {
      return {
        ...state,
        profileIcon: action.icon,
        profileIconType: action.iconType,
      };
    }
    case USER_EXPIRED:
    case LOAD_USER_ERROR:
    case SILENT_RENEW_ERROR:
    case USER_SIGNED_OUT:
      ReactDOM.render(
        <Alert textColor="white" width="16rem" height="4rem" color="#d05555">
          <i className="far fa-check-circle icon" /> Uživatel nepřihlášen
        </Alert>,
        document.getElementById("alerts")
      );
      ReactDOM.render(
        <div id="loginplsin">
          <Card className="loginpls">
            <button
              className="safer"
              onClick={() => {
                userManager.signinRedirect();
              }}
            >
              <p>Přihlásit se</p>
            </button>
          </Card>
        </div>,
        document.getElementById("loginpls")
      );
      return {
        ...state,
      };
    case SESSION_TERMINATED:
      return {
        ...state,
        idToken: null,
        accessToken: null,
        userId: null,
        profile: null,
        isUserLoading: false,
      };

    default: {
      return state;
    }
  }
};

export const ApplicationContext = createContext(initialState);
export const ApplicationConsumer = ApplicationContext.Consumer;
export const ApplicationProvider = (props) => {
  const store = useReducer(reducer, initialState);
  const [, dispatch] = store;
  useEffect(() => {
    userManager.events.addUserLoaded((user) => {
      const tokenData = parseJwt(user.access_token);
      dispatch({
        type: USER_FOUND,
        accessToken: user.access_token,
        idToken: user.id_token,
        userId: tokenData.sub,
        profile: user.profile,
      });

      axios({
        method: "post",
        url: "api/User/User",
        headers: { Authorization: "Bearer " + user.access_token },
        data: {
          oauthId: user.profile.sub,
          username: user.profile.preferred_username,
          firstName: user.profile.given_name,
          lastName: user.profile.family_name,
        },
      });
    });
    userManager.events.addUserUnloaded(() => {
      console.info("Informace o přihlášení jsou neplatné.");
    });
    userManager.events.addAccessTokenExpiring(() => {
      console.info("Platnost přihlášení brzy vyprší.");
    });
    userManager.events.addAccessTokenExpired(() => {
      console.info("Platnost přihlášení vypršela.");
    });
    userManager.events.addSilentRenewError(() => {
      console.info("Nepodařilo se obnovit přihlášení.");
    });
    userManager.events.addUserSignedOut(() => {
      console.info("Uživatel byl odhlášen.");
    });
    userManager
      .getUser()
      .then((user) => {
        if (user && !user.expired) {
          let tokenData = parseJwt(user.access_token);
          dispatch({
            type: USER_FOUND,
            accessToken: user.access_token,
            idToken: user.id_token,
            userId: tokenData.sub,
            profile: user.profile,
          });
          console.log("LOGIN");
          dispatch({
            type: UPDATE_PHOTO,
          });
        } else if (!user || (user && user.expired)) {
          dispatch({
            type: USER_EXPIRED,
          });
        }
      })
      .catch(() => {
        dispatch({
          type: LOAD_USER_ERROR,
        });
      });
  }, [dispatch]);
  return (
    <ApplicationContext.Provider value={store}>
      {props.children}
    </ApplicationContext.Provider>
  );
};
export const useAppContext = () => useContext(ApplicationContext);
