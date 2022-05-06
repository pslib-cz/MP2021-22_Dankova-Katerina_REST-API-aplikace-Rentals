import "moment/locale/cs";
import { useEffect, useState } from "react";
import Axios from "axios";
import { useAppContext } from "../../providers/ApplicationProvider";
import { OverlayTrigger, Popover } from "react-bootstrap";
import { Link } from "react-router-dom";
require("react-big-calendar/lib/css/react-big-calendar.css");

const Event = ({ event }) => {
  const [{ accessToken }] = useAppContext();
  const [items, setItems] = useState([]);

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

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }

  const AdminListItemDate = (props) => {
    if (props.state === 3) {
      return <p className="red">Zrušeno</p>;
    }
    if (props.state === 2) {
      return <p className="mygreen">Vráceno</p>;
    } else if (props.state === 1) {
      return <p className="mygreen">Vypůjčeno</p>;
    } else if (props.state === 0) {
      return <p className="myorange">Rezervované</p>;
    }
  };

  useEffect(() => {
    Axios.get("/api/Renting/" + event.id, config)
      .then((res) => {
        setItems(res.data.items);
      })
      .catch((err) => console.log(err)); // eslint-disable-next-line
  }, []);

  let popoverClickRootClose = (
    <Popover id="popover-trigger-click-root-close" style={{ zIndex: 10000 }}>
      <strong>{event.title}</strong>
      <AdminListItemDate state={event.state} />
      <p>
        {handleTime(event.start)} - {handleTime(event.end)}
      </p>
      {items ? <br /> : null}
      {items.map((item) => {
        return <p>{item.name}</p>;
      })}
      <br />
      <Link to={"renting/" + event.id}>Detail</Link>
      <br />
      <div className="arrow-down-popover"></div>
    </Popover>
  );

  return (
    <div>
      <div>
        <OverlayTrigger
          id="help"
          trigger="click"
          rootClose
          container={this}
          overlay={popoverClickRootClose}
        >
          <div>
            {event.title} |{" "}
            {items.map((item) => {
              return item.name + ", ";
            })}
          </div>
        </OverlayTrigger>
      </div>
    </div>
  );
};

export default Event;
