import styled from "styled-components";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "moment/locale/cs";
import { useEffect, useState } from "react";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";
import { useQuery } from "react-query";
import { StyledDetail } from "../Pages/Detail";
import { ImpulseSpinner } from "react-spinners-kit";
import { Badge, Card } from "proomkatest";
import Event from "./Event";
import { StyledSearchBox, StyledSearchBoxWithin } from "../Content/ContentMenu";
import { StyledMainGrid } from "../Content/Content";
require("react-big-calendar/lib/css/react-big-calendar.css");

const localizer = momentLocalizer(moment);

const StyledAdminCalendar = styled.div`
  width: ${(props) => (props.isSmall ? "90%" : "95%")};
  padding: ${(props) => (props.isSmall ? "5%" : "2.5%")};
  border-radius: 1.5rem;
  -webkit-box-shadow: 0 8px 20px 0px #d1d1d1;
  box-shadow: 0 8px 20px 0px #d1d1d1;

  background-color: white;
  text-align: center;
  height: ${(props) => (props.isHeight ? "40vh !important" : "85vh")};

  img {
    width: 100%;
    max-width: 100%;
    max-height: 100%;
  }

  @media (max-width: 900px) {
    margin-top: 4rem;
  }
`;

const StyledCategoryButton = styled.div`
  height: 4rem;
  width: auto;
  background-color: ${(props) => (props.clicked ? "#007784" : "#fff")};
  border-radius: 2.5rem;
  color: ${(props) => (props.clicked ? "white" : "unset")};

  box-shadow: rgb(0 0 0 / 23%) 0px 8px 20px 0px;

  display: grid;
  place-items: center;

  margin: 0 1rem;

  padding: 0 2rem;

  font-size: 1.25rem;
  font-weight: 500;

  cursor: default;

  transition: 250ms;

  &:hover {
    background-color: ${(props) => (props.clicked ? "#007784" : "#00a7b9")};
    color: white;
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

const messages = {
  allDay: "Celý den",
  previous: "<",
  next: ">",
  today: "Dnes",
  month: "Měsíc",
  week: "Týden",
  day: "Den",
  agenda: "Agenda",
  date: "Datum",
  time: "Čas",
  event: "Událost",
  showMore: (total) => `+ Zobrazit další (${total})`,
};

const AdminCalendar = (props) => {
  const [{ accessToken }] = useAppContext();
  const [dates, setDates] = useState([]);

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  const [isMyInputFocused, setIsMyInputFocused] = useState(false);

  const [SelectedCategories, setSelectedCategories] = useState([]);
  const [SelectedIds, setSelectedIds] = useState([]);

  const EditSelectedCategories = (newItem) => {
    if (SelectedCategories.indexOf(newItem) === -1) {
      const newArr = [...SelectedCategories, newItem];
      const newArr2 = [...SelectedIds, newItem.id];
      setSelectedCategories(newArr);
      setSelectedIds(newArr2);
    } else {
      setSelectedCategories(
        SelectedCategories.filter((item) => item !== newItem)
      );
      setSelectedIds(SelectedIds.filter((item) => item !== newItem.id));
    }
  };

  const FetchItems = () => {
    var formattedDates = [];
    return useQuery(
      "data",
      async () => {
        const { data } = await Axios.get(props.sources, config);
        data.forEach((i) => {
          formattedDates.push({
            start: new Date(i.start),
            end: new Date(i.end),
            title: i.title,
            id: i.id,
            state: i.state,
            items: [],
          });
        });
        return formattedDates;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken,
        refetchOnWindowFocus: false,
      }
    );
  };
  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setDates(uniq(data));
      Axios.get("/api/Item", config).then((data) => setAllItems(data.data));
    } // eslint-disable-next-line
  }, [status, data, accessToken]);

  useEffect(() => {}, []);

  function uniq(arr) {
    var copy = [];
    arr.forEach(function (item) {
      if (!copy.includes(item)) {
        copy.push(item);
      }
    });

    return copy;
  }

  const [allItems, setAllItems] = useState([]);
  useEffect(() => {
    Axios.get("/api/Item", config).then((data) => console.log(data.data)); // eslint-disable-next-line
  }, []);
  const [inputText, setInputText] = useState("");

  useEffect(() => {
    console.log(inputText);
  }, [inputText]);
  let inputHandler = (e) => {
    //convert input text to lower case
    var lowerCase = e.target.value.toLowerCase();
    setInputText(lowerCase);
  };

  const UpdateCalendar = () => {
    const [date, setDate] = useState(new Date());
    const [api, setApi] = useState("/api/Renting/Calendar");
    const [result, setResult] = useState([]);
    const [eventsToRender, setEventsToRender] = useState([]);

    useEffect(() => {
      setApi(
        `/api/Renting/Calendar?month=${
          date.getUTCMonth() + 1
        }&year=${date.getFullYear()}${SelectedIds.map((id) => {
          return "&items=" + id;
        }).join("")}`
      );
    }, [date]);

    useEffect(() => {
      Axios.get(api, config).then((data) => setResult(data.data));
    }, [api]);

    useEffect(() => {
      if (result.length) {
        async function changeEvent(arr) {
          var formatted = [];
          arr.forEach((i) => {
            // eslint-disable-next-line
            if (i.state != 3)
              formatted.push({
                start: new Date(i.start),
                end: new Date(i.end),
                title: i.title ? i.title : i?.owner?.fullName,
                id: i.id,
                state: i.state,
                items: [],
              });
          });
          return formatted;
        }

        changeEvent(result).then((res) => {
          setEventsToRender(uniq(res));
          console.log(uniq(res));
        });
      }
    }, [result]);

    if (status === "success") {
      return (
        <Calendar
          messages={messages}
          localizer={localizer}
          //jenom měsíc
          views={["month", "week", "day"]}
          events={eventsToRender}
          startAccessor="start"
          endAccessor="end"
          components={{
            event: Event,
          }}
          onNavigate={(date) => setDate(date)}
          eventPropGetter={(event) => {
            const backgroundColor =
              event.state === 0
                ? "#d0b055" // if
                : event.state === 1
                ? "#007784" // else if
                : event.state === 2
                ? "#00ae7c" // else if
                : event.state === 1
                ? "#d05555"
                : null; // else
            return { style: { backgroundColor } };
          }}
        />
      );
    }
    if (status === "loading") {
      return (
        <StyledDetail id="spinner">
          <ImpulseSpinner className="spinner" frontColor="#007784" size="64" />
        </StyledDetail>
      );
    }
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
  };

  const NormalCalendar = () => {
    if (status === "success") {
      return (
        <Calendar
          messages={messages}
          localizer={localizer}
          //jenom měsíc
          views={["month", "week", "day"]}
          events={dates}
          startAccessor="start"
          endAccessor="end"
          eventPropGetter={(event) => {
            const backgroundColor =
              event.state === 0
                ? "#d0b055" // if
                : event.state === 1
                ? "#007784" // else if
                : event.state === 2
                ? "#00ae7c" // else if
                : event.state === 1
                ? "#d05555"
                : null; // else
            return { style: { backgroundColor } };
          }}
        />
      );
    }
    if (status === "loading") {
      return (
        <StyledDetail id="spinner">
          <ImpulseSpinner className="spinner" frontColor="#007784" size="64" />
        </StyledDetail>
      );
    }
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
  };

  const target = document.querySelector("#searchField");

  document.addEventListener("click", (event) => {
    const withinBoundaries = event.composedPath().includes(target);

    if (withinBoundaries) {
      setIsMyInputFocused(true);
    } else {
      setIsMyInputFocused(false);
    }
  });

  return (
    <StyledMainGrid>
      {props.big ? (
        <div>
          <StyledSearchBox
            style={{ height: "auto", position: "absolute", zIndex: 5 }}
          >
            <StyledSearchBoxWithin>
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
                      obj.name.toLowerCase().indexOf(inputText.toLowerCase()) >=
                      0
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
          <div
            style={{
              marginTop: "6rem",
              width: "100%",
              display: "flex",
              flexWrap: "wrap",
            }}
          >
            {SelectedCategories.map((category, i) => (
              <div onClick={() => EditSelectedCategories(category)} key={i}>
                <StyledCategoryButton style={{ marginBottom: "1rem" }}>
                  <p>{category.name}</p>
                </StyledCategoryButton>
              </div>
            ))}
          </div>
        </div>
      ) : null}
      <StyledAdminCalendar
        isSmall={props.isSmall}
        isHeight={props.isHeight}
        {...props}
      >
        {props.big ? <UpdateCalendar /> : <NormalCalendar />}
      </StyledAdminCalendar>
    </StyledMainGrid>
  );
};

export default AdminCalendar;
