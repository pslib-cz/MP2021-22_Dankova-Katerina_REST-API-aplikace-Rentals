import AccountPhoto from "../AccountCard/AccountPhoto";
import { StyledMainGrid, StyledIntroImage } from "../Content/Content";
import wave from "../media/Wave.svg";

const Account = () => {
  return (
    <StyledMainGrid>
      <StyledIntroImage bgImage={wave}>
        <h1>Profil</h1>
      </StyledIntroImage>
      <AccountPhoto />
    </StyledMainGrid>
  );
};

export default Account;
