import styled from "styled-components";
import { useAppContext } from "../../providers/ApplicationProvider";

const StyledSignButton = styled.div`
  text-align: center;
  display: grid;
  justify-items: center;
  align-items: center;

  svg {
    margin-right: 1rem;
  }
`;

const SignButton = (props) => {
  const [{ userManager, accessToken }] = useAppContext();

  return (
    <StyledSignButton>
      {accessToken ? (
        <button
          color="primary"
          size="lg"
          onClick={() => {
            userManager.signoutRedirect();
          }}
        >
          Odhlásit se
        </button>
      ) : (
        <button
          color="primary"
          size="lg"
          onClick={() => {
            userManager.signinRedirect();
          }}
        >
          Přihlásit se
        </button>
      )}
    </StyledSignButton>
  );
};

export default SignButton;
