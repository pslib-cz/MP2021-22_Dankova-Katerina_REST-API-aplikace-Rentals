import {
  StyledMainGrid,
  StyledIntroImage,
  StyledContentGrid,
} from "../Content/Content";
import ContentMenu, {
  StyledDiv,
  StyledFilterButton,
} from "../Content/ContentMenu";
import wave from "../media/Wave.svg";
import { Card, Badge, Alert } from "proomkatest";
import { useState, useEffect } from "react";
import { useAppContext } from "../../providers/ApplicationProvider";
import CardImage from "../ContentCard/CardImage";
import Axios from "axios";
import ReactDOM from "react-dom";
import { useQuery, useQueryClient } from "react-query";

const Favourite = () => {
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);
  const [isFiltered, setIsFiltered] = useState(true);

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  const FetchItems = () => {
    return useQuery("favourites", async () => {
      const { data } = await Axios.get("/api/User/Favourites", config);
      ReactDOM.render(
        <Alert
          textColor="white"
          width="16rem"
          height="4rem"
          color="#00ae7c"
          delay="2000"
        >
          <i className="far fa-check-circle icon" /> Data aktualizována
        </Alert>,

        document.getElementById("ok")
      );
      return data;
    });
  };
  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setStoredFiles(data);
    }
  }, [status, data, accessToken]);

  const queryClient = useQueryClient();

  const handleClick = (id) => {
    Axios({
      method: "delete",
      url: "api/User/Favourites/" + id,
      headers: { Authorization: "Bearer " + accessToken },
    }).then(() => {
      queryClient.invalidateQueries("favourites");
    });
  };

  const filterFunction = (array) => {
    setIsFiltered(!isFiltered);
    setStoredFiles(array.reverse());
  };

  setTimeout(() => {
    ReactDOM.render(
      <StyledDiv>
        <StyledFilterButton
          isFiltered={isFiltered}
          onClick={() => filterFunction(storedFiles)}
        >
          <i className="fas fa-sort-amount-up-alt"></i>
        </StyledFilterButton>
      </StyledDiv>,
      document.getElementById("filter")
    );
  }, 250);

  const ApiReturned = () => {
    if (status === "success") {
      return (
        <StyledContentGrid>
          <>
            {storedFiles.map((i) => {
              return (
                <>
                  <Card key={i}>
                    <CardImage src={i.id}></CardImage>
                    <p className="card-header">{i.name}</p>
                    <p className="card-desc">{i.description}</p>
                    <Badge
                      color="#007784"
                      colorHover="#009fb1"
                      textColor="white"
                      onClick={() => handleClick(i.id)}
                    >
                      <i className="fas fa-heart"></i>
                    </Badge>
                  </Card>
                </>
              );
            })}
          </>
        </StyledContentGrid>
      );
    }
    if (status === "loading") {
      return (
        <StyledContentGrid className="liked-grid">
          <>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
            <Card>
              <div className="loading-img"></div>
              <p className="card-header loading"></p>
              <p className="card-desc loading"></p>
              <Badge
                className="loading proomka-badge"
                color="#007784"
                colorHover="#009fb1"
                textColor="white"
              >
                <i className="fas fa-cart-plus"></i>
              </Badge>
            </Card>
          </>
        </StyledContentGrid>
      );
    }

    if (status === "error") {
      return (
        <Card
          color="#d0af5529"
          width="100%"
          height="6rem"
          className="proomka-card empty"
        >
          <Badge
            top="1rem"
            right="1rem"
            colorHover="white"
            color="#ffc21c"
            textColor="white"
            textColorHover="#ffc21c"
          >
            <i className="fas fa-exclamation"></i>
          </Badge>
          Při načítání položek došlo k chybě
        </Card>
      );
    }
  };

  return (
    <StyledMainGrid>
      <StyledIntroImage bgImage={wave}>
        <h1>Oblíbené položky</h1>
      </StyledIntroImage>
      <ContentMenu></ContentMenu>
      <ApiReturned></ApiReturned>
    </StyledMainGrid>
  );
};

export default Favourite;
