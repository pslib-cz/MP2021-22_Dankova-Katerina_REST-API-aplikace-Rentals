import AdminList from "../Admin/AdminList";
import AdminListItem from "../Admin/AdminListItem";
import { default as BoldName } from "../Admin/AdminListItemName";
import { default as NormalName } from "../Admin/AdminListItemUser";
import { StyledMainGrid } from "../Content/Content";
import ContentMenu from "../Content/ContentMenu";
import Axios from "axios";
import { useEffect, useState } from "react";
import { useAppContext } from "../../providers/ApplicationProvider";
import { Badge, Card } from "proomkatest";
import { useHistory, useParams } from "react-router-dom";
import { ImpulseSpinner } from "react-spinners-kit";
import { StyledDetail } from "../Pages/Detail";
import { useQuery } from "react-query";
import useLongPress from "../helpers/UseLongPress";
import ToolTip from "../ToolTip/ToolTip";

const Item = (props) => {
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);

  const { id } = useParams();

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

  document.title = "Rentals | Admin";

  const FetchItems = () => {
    return useQuery(
      "admin-when",
      async () => {
        const { data } = await Axios.get(`/api/Item/History?id=${id}`, config);
        return data;
      },
      {
        // The query will not execute until the userId exists
        enabled: !!accessToken,
        refetchOnWindowFocus: false,
      }
    );
  };

  function handleTime(date) {
    var newDate = new Date(date);
    var formatDate = new Intl.DateTimeFormat("cs-cz", options).format(newDate);
    return formatDate;
  }

  function actionType(type) {
    switch (type) {
      case 0:
        return " Vytvoření";
      case 1:
        return " Změna: ";
      case 2:
        return " Změna příslušenství: ";
      case 3:
        return " Smazán";
      case 4:
        return " Navrácen";
      case 5:
        return " Přidán do inventáře";
      case 6:
        return " Odebrán z inventáře";
      default:
        return "";
    }
  }

  function actionType2(type) {
    switch (type) {
      case 0:
        return " Jméno";
      case 1:
        return " Kategorie";
      case 2:
        return " Popisek";
      case 3:
        return " Poznámka";
      case 4:
        return " Příslušenství";
      default:
        return "";
    }
  }

  const { status, data } = FetchItems();
  useEffect(() => {
    if (status === "success") {
      setStoredFiles(data);
      console.log(data);
    }
  }, [status, data, accessToken]);

  const Button3 = (props) => {
    let history = useHistory();

    const onLongPress = () => {
      navigator.vibrate(65);
      return history.push("/admin/list/" + id);
    };

    const open = () => {
      return history.push("/admin/list/" + id);
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
        <p className="green">Upravit</p>
      </button>
    );
  };
  

  if (status === "success") {
    return (
      <StyledMainGrid>
        <ContentMenu></ContentMenu>
        <AdminList isSmall>
          <AdminListItem>
            <BoldName name={"Předmět #" + id}></BoldName>
            <Button3 />
          </AdminListItem>
          <AdminListItem>
            <BoldName name={"Akce"}></BoldName>
            <BoldName name={"Kdy"}></BoldName>
            <BoldName name={"Kdo"}></BoldName>
            <BoldName name={"U koho je"}></BoldName>
          </AdminListItem>
          {storedFiles.map((i, index) => {
            return (
              <AdminListItem key={index}>
                <NormalName
                  name={
                    "#" +
                    index +
                    actionType(i?.itemHistoryLog?.action) +
                    i?.itemHistoryLog?.itemChanges?.map((i, index) => {
                      return actionType2(i?.changedProperty)
                    })
                  }
                ></NormalName>
                <NormalName
                  name={
                    i?.itemHistoryLog?.changedTime
                      ? handleTime(i?.itemHistoryLog?.changedTime)
                      : null
                  }
                ></NormalName>
                <NormalName
                  name={i?.itemHistoryLog?.user?.fullName}
                ></NormalName>
                <NormalName
                  name={i?.itemHistoryLog?.userInventory?.fullName}
                ></NormalName>
              </AdminListItem>
            );
          })}
        </AdminList>
      </StyledMainGrid>
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

export default Item;
