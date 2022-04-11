import Axios from "axios";
import { Card } from "proomkatest";
import { useState } from "react";
import { useHistory } from "react-router-dom";
import styled from "styled-components";
import { useAppContext } from "../../providers/ApplicationProvider";
import React, { useRef } from "react";
import * as tus from "tus-js-client";

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

const AdminAdd = (props) => {
  let history = useHistory();
  const [{ accessToken }] = useAppContext();
  const imputEl = useRef(null);
  const [name, setname] = useState("");
  const [category, setcategory] = useState();
  const [note, setnote] = useState("");
  const [desc, setdesc] = useState("");
  const [img, setImg] = useState("");

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

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

  const sendPatch = () => {
    Axios.post(
      "/api/Item/",
      {
        name: name,
        description: desc,
        note: note,
        img: img,
      },
      config
    ).then(history.push("/admin/list/"));
  };

  return (
    <StyledModal>
      <Modal onClick={() => history.push("/admin/list")}></Modal>
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
          <input
            ref={imputEl}
            type="file"
            name="filedata"
            onChange={(e) => {
              var file = e.target.files[0];

              // Create a new tus upload
              var upload = new tus.Upload(file, {
                // Endpoint is the upload creation URL from your tus server
                endpoint: "https://localhost:3000/files/",
                // Retry delays will enable tus-js-client to automatically retry on errors
                retryDelays: [0, 3000, 5000, 10000, 20000],
                // Attach additional meta data about the file for the server
                metadata: {
                  filename: file.name,
                  filetype: file.type,
                },
                // Callback for errors which cannot be fixed using retries
                onError: function (error) {
                  console.log("Failed because: " + error);
                },
                // Callback for reporting upload progress
                onProgress: function (bytesUploaded, bytesTotal) {
                  var percentage = ((bytesUploaded / bytesTotal) * 100).toFixed(
                    2
                  );
                  console.log(bytesUploaded, bytesTotal, percentage + "%");
                },
                // Callback for once the upload is completed
                onSuccess: function () {
                  setImg(upload.url.split("/").slice(-1).toString());
                },
              });

              upload.findPreviousUploads().then(function (previousUploads) {
                // Found previous uploads so we select the first one.
                if (previousUploads.length) {
                  upload.resumeFromPreviousUpload(previousUploads[0]);
                }

                // Start the upload
                upload.start();
              });
            }}
          />
          <button className="upgrade" onClick={() => sendPatch()} type="button">
            <p className="green">Přidat předmět</p>
          </button>
        </form>
      </Card>
    </StyledModal>
  );
};

export default AdminAdd;
