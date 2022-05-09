import { useEffect, useState } from "react";
import styled from "styled-components";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";
import BagCard from "../BagCard/BagCard";
import BagImage from "../BagCard/BagImage";
import BagText from "../BagCard/BagText";
import useLongPress from "../helpers/UseLongPress";
import { Badge } from "proomkatest";
import { StyledSearchBox, StyledSearchBoxWithin } from "../Content/ContentMenu";
import moment from "moment";

const StyledAdminListItem = styled.div`
  min-height: 2rem;
  height: auto;
  width: 100%;
  padding: 1rem 0;

  border-bottom: 1px grey solid;

  display: flex;
  justify-content: space-between;
  flex-direction: column;

  & > div > * {
    width: 100%;
  }

  & > div {
    display: flex;
    justify-content: space-between;

    @media (max-width: 1000px) {
      flex-direction: column;
      & > div > * {
        width: 100%;
        margin: 1rem 0 !important;
      }

      * {
        text-align: center !important;
        margin: 0;
        place-items: center;
      }
    }
  }

  @media (max-width: 1000px) {
    flex-direction: column;
    & > div > * {
      width: 100%;
      margin: 1rem 0 !important;
    }

    * {
      text-align: center !important;
      margin: 0;
      place-items: center;
    }
  }

  i {
    font-size: 2rem;
    align-self: center;

    transform: ${(props) => (!props.open ? "rotate(180deg)" : "rotate(0deg)")};
    transition: 250ms;
  }

  textarea,
  input {
    outline: none;
    border: none;
  }
`;

const SelectDiv = (props) => {
  return (
    <div
      className="searchp"
      onClick={() => {
        props.onClick();
      }}
    >
      {props.children}
    </div>
  );
};

const AdminListItem = (props) => {
  const [open, setOpen] = useState(false);
  const [{ accessToken }] = useAppContext();
  const [items, setItems] = useState([]);
  const [edit, setEdit] = useState(false);
  const [add, setAdd] = useState(false); // eslint-disable-next-line
  const [currentId, setCurrentId] = useState(props.setId);
  const [start, setStart] = useState(props.from);
  const [end, setEnd] = useState(props.to);

  function handleChange(event) {
    setStart(event.target.value);
  }
  function handleChange2(event) {
    setEnd(event.target.value);
  }

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  function uniq(arr) {
    var copy = [];
    arr.forEach(function (item) {
      if (!copy.includes(item)) {
        copy.push(item);
      }
    });

    return copy;
  }

  useEffect(() => {
    // eslint-disable-next-line
    props?.kid?.map((item) => {
      Axios.get("/api/item/" + item.itemId, config).then((res) => {
        const arr = items;
        arr.push(res.data);
        setItems(arr);
      });
    }); // eslint-disable-next-line
  }, [props.kid]);

  const [SelectedIds, setSelectedIds] = useState([]);

  const EditSelectedCategories = (newItem) => {
    if (items.indexOf(newItem) === -1) {
      const newArr = [...items, newItem];
      const newArr2 = [...SelectedIds, newItem.id];
      setItems(newArr);
      setSelectedIds(newArr2);
    } else {
      setItems(items.filter((item) => item !== newItem));
      setSelectedIds(SelectedIds.filter((item) => item !== newItem.id));
    }
  };

  useEffect(() => {
    console.log(uniq(items));
  }, [items]);

  const [allItems, setAllItems] = useState([]);
  useEffect(() => {
    Axios.get("/api/Item", config).then((data) => setAllItems(data.data)); // eslint-disable-next-line
  }, []);

  const Button = (props) => {
    const onLongPress = () => {
      setEdit(true);
      navigator.vibrate(65);
    };

    const open = () => {
      navigator.vibrate(65);
      setEdit(true);
    };

    const onClick = () => {
      open();
    };

    const defaultOptions = {
      shouldPreventDefault: true,
      delay: 500,
    };
    const longPressEvent = useLongPress(onLongPress, onClick, defaultOptions);
    return (
      <button {...longPressEvent} className="upgrade">
        <p className="yellow">Upravit</p>
      </button>
    );
  };
  const Button2 = (props) => {
    const onLongPress = () => {
      setAdd(true);
      navigator.vibrate(65);
    };

    const open = () => {
      navigator.vibrate(65);
      setAdd(true);
    };

    const onClick = () => {
      open();
    };

    const defaultOptions = {
      shouldPreventDefault: true,
      delay: 500,
    };
    const longPressEvent = useLongPress(onLongPress, onClick, defaultOptions);
    return (
      <button {...longPressEvent} className="upgrade">
        <p className="yellow">Přidat</p>
      </button>
    );
  };
  const Button3 = (props) => {
    const onLongPress = () => {
      Axios.put(
        "/api/renting/change",
        {
          rentingId: currentId,
          items: items.map((item) => {
            return item.id;
          }),
          start: start,
          end: end,
        },
        {
          headers: {
            Authorization: "Bearer " + accessToken,
          },
        }
      );
      setEdit(false);
      navigator.vibrate(65);
    };

    const open = () => {
      Axios.put(
        "/api/renting/change",
        {
          rentingId: currentId,
          items: items.map((item) => {
            return item.id;
          }),
          start: start,
          end: end,
        },
        {
          headers: {
            Authorization: "Bearer " + accessToken,
          },
        }
      );
      navigator.vibrate(65);
      setEdit(false);
    };

    const onClick = () => {
      open();
    };

    const defaultOptions = {
      shouldPreventDefault: true,
      delay: 500,
    };
    const longPressEvent = useLongPress(onLongPress, onClick, defaultOptions);
    return (
      <button {...longPressEvent} className="upgrade">
        <p className="red">Přestat upravovat</p>
      </button>
    );
  };

  const [isMyInputFocused, setIsMyInputFocused] = useState(false);
  const [inputText, setInputText] = useState("");

  useEffect(() => {
    console.log(inputText);
  }, [inputText]);
  let inputHandler = (e) => {
    //convert input text to lower case
    var lowerCase = e.target.value.toLowerCase();
    setInputText(lowerCase);
  };

  const target = document.querySelector("#searchFieldId");

  document.addEventListener("click", (event) => {
    const withinBoundaries = event.composedPath().includes(target);

    if (withinBoundaries) {
      setIsMyInputFocused(true);
    } else {
      setIsMyInputFocused(false);
    }
  });

  if (props.detail) {
    return (
      <StyledAdminListItem className="item-list" open={open}>
        <div>
          {props.children}
          <i className="fas fa-chevron-down" onClick={() => setOpen(!open)}></i>
        </div>
        {open ? (
          <div
            style={{
              display: "flex",
              flexDirection: "column",
            }}
          >
            {items.map((item, i) => (
              <div key={i} style={{ display: "flex", flexDirection: "row" }}>
                <BagCard>
                  <BagImage src={item.id}></BagImage>
                  <BagText
                    file={item.id}
                    text="Nazev produktu"
                    description="Popisek produktu"
                  ></BagText>
                </BagCard>
                {props.edit === 0 && edit ? (
                  <div
                    className="trashdiv"
                    onClick={() => EditSelectedCategories(item)}
                  >
                    <Badge color="#fff" textColorHover="crimson">
                      <i className="fas fa-trash"></i>
                    </Badge>
                  </div>
                ) : null}
              </div>
            ))}
            {props.edit === 0 && !edit ? (
              <div>
                <Button />
              </div>
            ) : props.edit === 0 && edit ? (
              <>
                <span>
                  <br />
                  Od:
                  <input
                    type="datetime-local"
                    id="meeting-time"
                    name="meeting-time"
                    onChange={handleChange}
                    min={moment().format("YYYY-MM-DDThh:mm")}
                  />
                  <br /> Do:{" "}
                  <input
                    type="datetime-local"
                    id="meeting-time"
                    name="meeting-time"
                    onChange={handleChange2}
                    min={moment().format("YYYY-MM-DDThh:mm")}
                  />
                </span>
                {add ? (
                  <div>
                    <StyledSearchBox
                      style={{ height: "auto", position: "static", zIndex: 6 }}
                    >
                      <StyledSearchBoxWithin id="searchFieldId">
                        <input
                          id="searchField"
                          type="text"
                          placeholder="Hledat předmět..."
                          onChange={inputHandler}
                          onFocus={() => setIsMyInputFocused(true)}
                        />
                        <span>
                          <i className="fas fa-search"></i>
                        </span>
                      </StyledSearchBoxWithin>
                      {isMyInputFocused
                        ? allItems
                            .filter(
                              (obj) =>
                                obj.name
                                  .toLowerCase()
                                  .indexOf(inputText.toLowerCase()) >= 0
                            )
                            .map((item, i) => {
                              return (
                                <SelectDiv
                                  key={i}
                                  onClick={() => {
                                    EditSelectedCategories(item);
                                  }}
                                >
                                  <img
                                    style={{ height: "3rem" }}
                                    alt="pic"
                                    src={"/api/Item/Img/" + item.id}
                                    onError={(e) => {
                                      e.currentTarget.src = "/image.svg";
                                    }}
                                  />
                                  <p>{item.name}</p>
                                </SelectDiv>
                              );
                            })
                        : null}
                    </StyledSearchBox>
                  </div>
                ) : null}
                <div>
                  <Button2 />
                  <Button3 />
                </div>
              </>
            ) : null}
          </div>
        ) : null}
      </StyledAdminListItem>
    );
  } else {
    return (
      <StyledAdminListItem className="item-list" open={open}>
        <div>{props.children}</div>
      </StyledAdminListItem>
    );
  }
};

export default AdminListItem;
