import styled from "styled-components";
import AdminCalendar from "../Admin/AdminCalendar";
import { useParams } from "react-router";
import { StyledMainGrid, StyledContentGrid } from "../Content/Content";
import { Card, Badge, Alert } from "proomkatest";
import { useEffect, useState } from "react";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";
import { ImpulseSpinner } from "react-spinners-kit";
import ReactDOM from "react-dom";
import CardImage from "../ContentCard/CardImage";
import { useQuery, useQueryClient } from "react-query";
import { useHistory } from "react-router-dom";

export const StyledDetail = styled.div`
  width: 100%;
  height: auto;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  row-gap: 32px;
  column-gap: 32px;
  align-items: center;
  font-size: 1rem;

  &#spinner {
    position: fixed;
    text-align: center;
    grid-template-columns: unset;
    width: 80%;
    place-items: center;
    height: 95vh;
  }

  h1 {
    font-size: 1.5rem;
  }
  h2 {
    padding-top: 1rem;
    font-size: 1.25rem;
    font-weight: 500;
  }

  @media (max-width: 800px) {
    grid-template-columns: repeat(1, 1fr);
    width: 100%;
    padding: 0;
  }

  img {
    max-height: 40vh;
    max-width: 100%;
    object-fit: contain;
    border-radius: 1.5rem;
    -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
    box-shadow: 0 8px 20px 0px #d1d1d1;
    justify-self: end;

    @media (max-width: 800px) {
      justify-self: center;
    }
  }

  .button {
    font-size: 0.75rem;
  }

  .center {
    height: auto;
    @media (max-width: 800px) {
      display: grid;
      align-items: center;
      grid-template-columns: repeat(2, auto);
    }
  }
`;

const StyledDetailText = styled.div`
  text-align: left;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  @media (max-width: 800px) {
    width: auto;
    justify-self: center;
    padding: 0 5%;
  }
`;

const ActionButton = styled.div`
  float: left;

  text-align: center;
  background-color: #007784;
  padding: 1em;
  border-radius: 2.5rem;
  color: #fff;
  cursor: pointer;
  transition: 100ms;
  min-width: 11em;
  margin-right: auto;
  margin-top: 1em;
  -webkit-box-shadow: 0 8px 20px 0px #8d8d8d;
  box-shadow: 0 8px 20px 0px #8d8d8d;

  &:hover {
    background-color: #198490;
    transition: 100ms;
  }

  @media (max-width: 800px) {
    margin-right: 0;
  }
`;

const StyledCardLike = styled.span`
  float: left;
  margin-top: 1em;
  padding: 0.75em 0 0.75em 0.75em;

  i {
    font-size: 2rem;
    color: ${(props) => (props.isLiked ? "#007784" : "#00000024")};
    transition: 150ms;

    &:hover {
      color: #007784;
      transition: 150ms;
      cursor: pointer;
      transform: scale(1.15);
    }
  }
`;

const StyledCalendarGrid = styled.div`
  margin-top: 6rem;

  h3 {
    text-align: center;
    padding: 1rem 0 3rem 0;
  }
`;

const Detail = (props) => {
  const { id } = useParams();
  const [{ accessToken }] = useAppContext();
  const [item, setItem] = useState({});
  const [like, setLike] = useState(false);
  const [connectedFiles, setConnectedFiles] = useState([]);
  const [src, setSrc] = useState();
  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  let history = useHistory();

  useEffect(() => {
    if (accessToken) {
      console.log(accessToken);
    }
  }, [accessToken]);

  const FetchItem = () => {
    return useQuery(
      "detail",
      async () => {
        const { data } = await Axios.get("/api/Item/" + id, config);
        return data;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken,
        refetchOnWindowFocus: false,
      }
    );
  };
  const { status, data } = FetchItem();
  const queryClient = useQueryClient();

  useEffect(() => {
    console.log("I have changed to: " + id);

    queryClient.removeQueries("detail", { exact: true });
    // eslint-disable-next-line
  }, [id]);

  useEffect(() => {
    if (status === "success") {
      document.title = `Rentals | ${data.name}`;
      setItem(data);
    }
  }, [status, data]);

  const fetchLike = async () => {
    const { data } = await Axios.get(
      `${process.env.REACT_APP_CLIENT_URL}/api/Item/IsFavourite/` + id,
      config
    );
    setLike(data);
    console.log("like", data);
    console.log(like);
  };

  useEffect(() => {
    const fetchConnectedFiles = async () => {
      const { data } = await Axios.get(
        `${process.env.REACT_APP_CLIENT_URL}/api/Item/Accesories/` + id,
        config
      );
      setConnectedFiles(data);
      console.log("data", data);
    };

    fetchConnectedFiles();
    fetchLike();

    // eslint-disable-next-line
  }, [accessToken]);

  useEffect(() => {
    setSrc(`/api/Item/${id}/Dates`);
  }, [id]);

  const handleClick = (id) => {
    Axios({
      method: "post",
      url: "/api/User/Favourites/" + id,
      headers: { Authorization: "Bearer " + accessToken },
    }).then(
      fetchLike(),
      ReactDOM.unmountComponentAtNode(document.getElementById("ok")),
      ReactDOM.render(
        <Alert
          textColor="white"
          width="16rem"
          height="4rem"
          color="#00ae7c"
          delay="2000"
        >
          <i className="far fa-check-circle icon" /> Přidáno do oblíbených
        </Alert>,

        document.getElementById("ok")
      )
    );
  };

  const handleClick2 = (id) => {
    Axios({
      method: "delete",
      url: "/api/User/Favourites/" + id,
      headers: { Authorization: "Bearer " + accessToken },
    }).then(
      fetchLike(),
      ReactDOM.unmountComponentAtNode(document.getElementById("ok")),
      ReactDOM.render(
        <Alert
          textColor="white"
          width="16rem"
          height="4rem"
          color="#d05555"
          delay="2000"
        >
          <i className="far fa-check-circle icon" /> Odebráno z oblíbených
        </Alert>,

        document.getElementById("ok")
      )
    );
  };

  if (status === "success") {
    return (
      <>
        <StyledDetail>
          <img
            src={"/api/Item/Img/" + id}
            alt="detailedpicture"
            onError={(e) => {
              e.currentTarget.src = "/image.svg";
            }}
          />
          <StyledDetailText>
            <h1>{item.name}</h1>
            <div>
              <h2>Specifikace</h2>
              <p>{item.note}</p>
            </div>
            <div>
              <h2>Popisek</h2>
              <p>{item.description}</p>
            </div>
            <div className="center">
              <ActionButton
                onClick={() =>
                  Axios({
                    method: "post",
                    url: "/api/User/Cart/" + id,
                    headers: { Authorization: "Bearer " + accessToken },
                  })
                    .then(function (response) {
                      console.log(response);
                      history.push("/bag");
                    })
                    .catch(function (error) {
                      console.log(error);
                    })
                }
              >
                Přidat do košíku <i className="fas fa-shopping-bag"></i>
              </ActionButton>
              {like ? (
                <StyledCardLike>
                  <i
                    className="fas fa-heart filled-heart"
                    onClick={() => handleClick2(id)}
                  ></i>
                </StyledCardLike>
              ) : (
                <StyledCardLike>
                  <i
                    className="fas fa-heart"
                    onClick={() => handleClick(id)}
                  ></i>
                </StyledCardLike>
              )}
            </div>
          </StyledDetailText>
        </StyledDetail>
        <StyledCalendarGrid>
          <h3>Dostupnost</h3>
          <AdminCalendar sources={src}></AdminCalendar>
        </StyledCalendarGrid>
        <StyledMainGrid>
          <h3>Kompatibilní příslušenství</h3>
          {connectedFiles.length ? (
            <StyledContentGrid>
              {connectedFiles.map((i) => {
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
                      >
                        <i className="fas fa-cart-plus"></i>
                      </Badge>
                    </Card>
                  </>
                );
              })}
            </StyledContentGrid>
          ) : (
            <p>Není žádné kompatibilní příslušenství</p>
          )}
        </StyledMainGrid>
      </>
    );
  }

  if (status === "loading") {
    return (
      <StyledDetail id="spinner">
        <ImpulseSpinner className="spinner" frontColor="#007784" size="64" />
      </StyledDetail>
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

  return (
    <StyledDetail id="spinner">
      <ImpulseSpinner className="spinner" frontColor="#007784" size="64" />
    </StyledDetail>
  );
};

export default Detail;
