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
            title: i.title + " - ID: #" + i.id,
            id: i.id,
            state: i.state,
          });
        });
        return formattedDates;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken, refetchOnWindowFocus: false,
      }
    );
  };
  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setDates(uniq(data));
    }
  }, [status, data, accessToken]);

  function uniq(arr) {
    var copy = [];
    arr.forEach(function (item) {
      if (!copy.includes(item)) {
        copy.push(item);
      }
    });

    return copy;
  }

  const UpdateCalendar = () => {
    if (status === "success") {
      return (
        <Calendar
          messages={messages}
          localizer={localizer}
          view="month"
          //jenom měsíc
          views={["month"]}
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

  return (
    <StyledAdminCalendar
      isSmall={props.isSmall}
      isHeight={props.isHeight}
      {...props}
    >
      <UpdateCalendar />
    </StyledAdminCalendar>
  );
};

export default AdminCalendar;
