import React from "react";
import { Redirect } from "react-router";
import { useAppContext } from "../../providers/ApplicationProvider";

const SignInCallback = (props) => {
  const [{ userManager }] = useAppContext();

  userManager.signinRedirectCallback();

  return <Redirect to="/" />;
};

export default SignInCallback;
