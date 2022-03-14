import styled from "styled-components";
import { useAppContext } from "../../providers/ApplicationProvider";
import { useEffect, useState } from "react";
import { Card } from "proomkatest";
import AccountCard from "../AccountCard/AccountCard";
import AccountCardDate from "../AccountCard/AccountCardDate";
import BagText from "../BagCard/BagText";
import Axios from "axios";

const StyledAccountPhoto = styled.div`
  display: flex;
  flex-direction: row;
  height: auto;
  width: 98%;
  margin: 0 1%;
  gap: 2rem;

  .img-wrapper {
    width: auto;
    max-width: 100%;
    border-radius: 1.5rem;
    display: flex;
    flex-direction: flex;
    justify-content: space-between;
    height: min-content;

    img {
      margin: 2rem;
      border-radius: 1.5rem;
      max-width: 50%;
      aspect-ratio: 1/1;
    }

    @media (max-width: 1600px) {
      flex-direction: column;
      img {
        max-width: 100%;
      }
    }
    @media (max-width: 1280px) {
      flex-direction: row;
      img {
        max-width: 50%;
      }
    }
    @media (max-width: 1000px) {
      flex-direction: column;
      img {
        max-width: 100%;
      }
    }

    div {
      margin-top: 1.5rem;
      display: flex;
      flex-direction: column;
      justify-content: center;
    }

    p {
      margin: 0 5%;
    }
    p:first-of-type {
      margin-top: 0 !important;
    }
    p:nth-child(odd) {
      color: #0000009d;
      font-size: 1.25rem;
      margin-top: 0.5rem;
      font-weight: 600;
    }
    p:nth-child(even) {
      color: #000000;
      font-size: 1.5rem;
      font-weight: 500;
    }
    p:last-child {
      margin-bottom: 2rem;
    }
  }
  .div-wrapper {
    width: 50%;
    display: flex;
    flex-direction: column;
    gap: 2rem;

    .proomka-card {
      width: 100%;
      background-color: #fff;
      border-radius: 1.5rem;
      transition: 250ms;
      display: flex;
      justify-content: space-between;
      height: auto;

      @media (max-width: 700px) {
        min-height: 6rem;
        width: 100%;
      }

      @media (max-width: 600px) {
        min-height: 5rem;
      }
    }
  }

  &.space {
    justify-content: space-between;
  }

  .history {
    width: 100%;
    border-radius: 1.5rem;
    flex-direction: column;
  }

  @media (max-width: 1280px) {
    flex-direction: column;
  }
`;

const AccountPhoto = (props) => {
  const [{ accessToken, userManager, profile }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };
  var options = {
    month: "short",
    day: "numeric",
    year: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  };

  const fetchStoredFiles = async (profile) => {
    try {
      const { data } = await Axios.get(
        "/api/Renting/RentingsByUser/" + profile.sub,
        config
      );
      setStoredFiles(data);
      console.log("data", data);
    } catch {
      console.log("Profile isn't loaded");
    }
  };

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }

  useEffect(() => {
    setTimeout(() => {
      document.getElementById("myImg2").src =
        document.getElementById("myImg").src;
      fetchStoredFiles(profile);
    }, 500);

    // eslint-disable-next-line
  }, [profile]);

  return (
    <StyledAccountPhoto {...props} id="myImg2cont">
      {accessToken ? (
        <>
          <Card className="proomka-card img-wrapper">
            <img
              src=""
              id="myImg2"
              alt="login"
              className="login"
              onClick={() => {
                userManager.signoutRedirect();
              }}
            ></img>
            <div>
              <p>Jméno</p>
              <p>{profile.name}</p>
              <p>Email</p>
              <p>{profile.email}</p>
              <p>Preferované jméno</p>
              <p>{profile.preferred_username}</p>
              <p>Id</p>
              <p>{profile.sub}</p>
            </div>
          </Card>
          <div className="history">
            {storedFiles.map((file, i) => (
              <AccountCard id={file.id} isBordered items={file.items}>
                <BagText
                  text={"Vypujcka #" + file.id}
                  description={
                    handleTime(file.start) + " - " + handleTime(file.end)
                  }
                ></BagText>
                <AccountCardDate state={file.state}></AccountCardDate>
              </AccountCard>
            ))}
          </div>
        </>
      ) : (
        <i
          className="fas fa-user login"
          onClick={() => {
            userManager.signinRedirect();
          }}
        ></i>
      )}
    </StyledAccountPhoto>
  );
};

export default AccountPhoto;
