import AdminList from "../Admin/AdminList";
import AdminListItem from "../Admin/AdminListItem";
import AdminListItemUser from "../Admin/AdminListItemUser";
import { StyledMainGrid } from "../Content/Content";
import ContentMenu from "../Content/ContentMenu";
import Axios from "axios";
import { useEffect, useState } from "react";
import { useAppContext } from "../../providers/ApplicationProvider";
import { Route, Switch, useHistory } from "react-router-dom";
import Inventory from "./Inventory";

const AdminUsers = (props) => {
  const [storedFiles, setStoredFiles] = useState([]);
  const [{ accessToken }] = useAppContext();
  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  document.title = "Rentals | Uživatelé";

  const fetchStoredFiles = async () => {
    const { data } = await Axios.get("/api/User", config);
    console.log(data);

    setStoredFiles(data);
  };

  useEffect(() => {
    fetchStoredFiles();
    // eslint-disable-next-line
  }, [accessToken]);

  let history = useHistory();

  return (
    <StyledMainGrid>
      <Switch>
        <Route path="/admin/users/:id">
          <Inventory />
        </Route>
      </Switch>
      <ContentMenu></ContentMenu>

      {storedFiles.map((i, index) => {
        return (
          <>
            <AdminList isSmall key={index}>
              <AdminListItem>
                <button
                  className="upgrade"
                  onClick={() => history.push("/admin/users/" + i.oauthId)}
                >
                  <p className="green">Inventář</p>
                </button>
                <AdminListItemUser name={i.fullName}></AdminListItemUser>
              </AdminListItem>
            </AdminList>
          </>
        );
      })}
    </StyledMainGrid>
  );
};

export default AdminUsers;
