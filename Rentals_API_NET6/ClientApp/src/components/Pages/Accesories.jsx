import Axios from "axios";
import { Badge, Card } from "proomkatest";
import { useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import { ImpulseSpinner } from "react-spinners-kit";
import styled from "styled-components";
import { useAppContext } from "../../providers/ApplicationProvider";
import { StyledContentGrid } from "../Content/Content";
import CardImage from "../ContentCard/CardImage";
import { StyledDetail } from "./Detail";

const Modal = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  z-index: 6;
  width: 100vw;
  height: 100vh;
  background-color: #000000c3;
  backdrop-filter: blur(10px);
  transition: 250ms;
  display: grid;
  place-items: center;
  cursor: pointer;
`;
const StyledModal = styled.div`
  .modal-card {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    margin: auto;
    z-index: 7;
    cursor: default;

    width: 80vw;
    height: 80vh;

    display: grid;
    place-items: start;

    text-align: center;

    overflow: scroll;
  }
`;

const Accesories = (props) => {
  let history = useHistory();
  const [{ accessToken }] = useAppContext();
  const { id } = useParams();
  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState([]);

  useEffect(() => {
    const config = {
      headers: {
        Authorization: "Bearer " + accessToken,
      },
    };

    const fetchUser = async () => {
      const { data } = await Axios.get("/api/Item/Accesories/" + id, config);
      setUser(data);
      setLoading(false);
    };
    fetchUser().then((document.title = `Rentals | Inventář ${user.name}`));

    // eslint-disable-next-line
  }, [id, accessToken]);

  const sendPut = () => {
    Axios.put(
      "/api/Item/Accesories",
      {
        id: parseInt(id),
        items: user.map((i) => i.id),
      },
      {
        headers: {
          Authorization: "Bearer " + accessToken,
        },
      }
    );
  };

  return (
    <StyledModal>
      <Modal onClick={() => history.push("/admin/list/" + id)}></Modal>
      {!loading ? (
        <Card className="modal-card">
          <StyledContentGrid>
            {user.length ? (
              <>
                {user.map((i, index) => {
                  return (
                    <>
                      <Card key={i}>
                        <CardImage src={i.id}></CardImage>
                        <p className="card-header">{i.name}</p>
                        <p className="card-desc">{i.description}</p>
                        <Badge
                          color="#d05555"
                          colorHover="#c41d1d"
                          textColor="white"
                          onClick={() => {
                            setUser(user.filter((value) => value.id !== i.id));
                            sendPut();
                          }}
                        >
                          <i className="fas fa-times"></i>
                        </Badge>
                      </Card>
                    </>
                  );
                })}
              </>
            ) : (
              <h3>Žádné předměty</h3>
            )}
          </StyledContentGrid>
        </Card>
      ) : (
        <Card className="modal-card">
          <StyledDetail id="spinner">
            <ImpulseSpinner
              className="spinner"
              frontColor="#007784"
              size="64"
            />
          </StyledDetail>
        </Card>
      )}
    </StyledModal>
  );
};

export default Accesories;
