import Axios from "axios";
import { Card } from "proomkatest";
import { useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import { ImpulseSpinner } from "react-spinners-kit";
import styled from "styled-components";
import { useAppContext } from "../../providers/ApplicationProvider";
import { StyledDetail } from "./Detail";
var rfc6902 = require("rfc6902");

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
    place-items: center;

    text-align: center;
  }

  form {
    width: 80%;
    display: flex;
    flex-direction: column;
    gap: 1.5rem;

    input {
      width: 80%;
    }

    label {
      font-weight: 700;

      &.head {
        display: unset;
        font-weight: 700;
      }
    }

    div {
      display: flex;
      flex-direction: column;
      width: 80%;
      gap: 0.5rem;
      margin: auto;
      input {
        width: unset;
      }

      label {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        font-weight: unset;

        input {
          margin-top: 6px;
        }
      }
    }
  }
`;

const AdminEditItems = (props) => {
  let history = useHistory();
  const [{ accessToken }] = useAppContext();
  const { id } = useParams();
  const [item, setItem] = useState({});
  const [loading, setLoading] = useState(true);

  const [name, setname] = useState("");
  const [category, setcategory] = useState();
  const [note, setnote] = useState("");
  const [desc, setdesc] = useState("");

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  document.title = `Rentals | Úprava ${id}`;

  useEffect(() => {
    const fetchItem = async () => {
      const { data } = await Axios.get("/api/Item/" + id, config).then(
        setLoading(false)
      );
      console.log(data);
      setItem(data);
      setname(data.name);
      setnote(data.note);
      setdesc(data.description);
      setcategory(data.categoryId);
    };
    fetchItem();

    // eslint-disable-next-line
  }, [id, accessToken]);

  const handleChangeName = (event) => {
    setname(event.target.value);
  };
  const handleChangeCategory = (event) => {
    setcategory(parseInt(event.target.value));
  };
  const handleChangeNote = (event) => {
    setnote(event.target.value);
  };
  const handleChangeDesc = (event) => {
    setdesc(event.target.value);
  };

  const body = rfc6902.createPatch(item, {
    id: parseInt(id),
    name: name,
    description: desc,
    note: note,
    imgFile: item.imgFile,
    img: item.img,
    isDeleted: item.isDeleted,
    state: item.state,
    rentings: item.rentings,
    categoryId: category,
    category: item.category,
    inventories: item.inventories,
    favourites: item.favourites,
    accessories: item.accessories,
    carts: item.carts,
  });

  const sendPatch = () => {
    Axios.patch("/api/Item/" + id, body, config);
  };
  const sendDelete = () => {
    Axios.patch(
      "/api/Item/Delete/" + id,
      rfc6902.createPatch(item, {
        id: parseInt(id),
        name: name,
        description: desc,
        note: note,
        imgFile: item.imgFile,
        img: item.img,
        isDeleted: "true",
        state: item.state,
        rentings: item.rentings,
        categoryId: category,
        category: item.category,
        inventories: item.inventories,
        favourites: item.favourites,
        accessories: item.accessories,
        carts: item.carts,
      }),
      config
    );
  };

  return (
    <StyledModal>
      <Modal onClick={() => history.push("/admin/list")}></Modal>
      {!loading ? (
        <Card className="modal-card">
          <form>
            <label htmlFor="editName">
              Jméno
              <br />
              <input
                type="text"
                name="Name"
                value={name}
                onChange={handleChangeName}
                id="editName"
              />
            </label>

            <div>
              <label className="head">Kategorie</label>
              <label for="cat1">
                Přístroje
                <input
                  type="radio"
                  id="cat1"
                  name="age"
                  value={1}
                  checked={category === 1}
                  onClick={handleChangeCategory}
                />
              </label>
              <label for="cat2">
                Objektivy
                <input
                  type="radio"
                  id="cat2"
                  name="age"
                  value={2}
                  checked={category === 2}
                  onClick={handleChangeCategory}
                />
              </label>
              <label for="cat3">
                Stativy
                <input
                  type="radio"
                  id="cat3"
                  name="age"
                  value={3}
                  checked={category === 3}
                  onClick={handleChangeCategory}
                />
              </label>
              <label for="cat4">
                Příslušenství
                <input
                  type="radio"
                  id="cat4"
                  name="age"
                  value={4}
                  checked={category === 4}
                  onClick={handleChangeCategory}
                />
              </label>
              <label for="cat5">
                Audiotechnika
                <input
                  type="radio"
                  id="cat5"
                  name="age"
                  value={5}
                  checked={category === 5}
                  onClick={handleChangeCategory}
                />
              </label>
              <label for="cat6">
                Ostatní
                <input
                  type="radio"
                  id="cat6"
                  name="age"
                  value={6}
                  checked={category === 6}
                  onClick={handleChangeCategory}
                />
              </label>
            </div>

            <label htmlFor="editNote">
              Note
              <br />
              <input
                type="text"
                name="Note"
                value={note}
                onChange={handleChangeNote}
                id="editNote"
              />
            </label>
            <label htmlFor="editDesc">
              Desc
              <br />
              <input
                type="text"
                name="Note"
                value={desc}
                onChange={handleChangeDesc}
                id="editDesc"
              />
            </label>
            {item.isDeleted ? (
              <button className="upgrade" type="button">
                <p className="red">Obnovit předmět</p>
              </button>
            ) : (
              <button
                className="upgrade"
                type="button"
                onClick={() => sendDelete()}
              >
                <p className="red">Smazat předmět</p>
              </button>
            )}
            <button
              className="upgrade"
              onClick={() => sendPatch()}
              type="button"
            >
              <p className="green">Uložit změny</p>
            </button>
          </form>
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

export default AdminEditItems;
