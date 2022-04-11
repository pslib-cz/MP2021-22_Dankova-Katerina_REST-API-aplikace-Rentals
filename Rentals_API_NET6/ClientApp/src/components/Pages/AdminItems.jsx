import {
  StyledMainGrid,
  StyledIntroImage,
  StyledcategoryWrapper,
  StyledContentGrid,
} from "../Content/Content";
import ContentMenu, {
  StyledDiv,
  StyledFilterButton,
  StyledSearchBox,
  StyledSearchBoxWithin,
} from "../Content/ContentMenu";
import wave from "../media/Wave.svg";
import { Card, Badge } from "proomkatest";
import { useState, useEffect } from "react";
import { useAppContext } from "../../providers/ApplicationProvider";
import CardImage from "../ContentCard/CardImage";
import Axios from "axios";
import ReactDOM from "react-dom";
import CategoryButton from "../Button/CategoryButton";
import { Route, Switch, useHistory } from "react-router-dom";
import AdminEditItems from "./AdminEditItems";
import { useQuery } from "react-query";
import Button from "../Button/Button";
import AdminAdd from "./AdminAdd";

const AdminItems = (props) => {
  const [{ accessToken }] = useAppContext();
  const [storedFiles, setStoredFiles] = useState([]);
  const [deletedFiles, setDeletedFiles] = useState([]);
  const [isFiltered, setIsFiltered] = useState(true);

  const config = {
    headers: {
      Authorization: "Bearer " + accessToken,
    },
  };

  let history = useHistory();

  document.title = "Rentals | Admin předměty";

  const [Categrories, updateCategories] = useState([]);
  const FetchItems = () => {
    return useQuery(
      "items",
      async () => {
        const { data } = await Axios.get("/api/Item", config);
        return data;
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
      setStoredFiles(data);
    }
  }, [status, data, accessToken]);

  const handleClick = (id) => {
    history.push("/admin/list/" + id);
  };
  const handleDelete = (id) => {
    Axios({
      method: "put",
      url: "/api/Item/Restore/" + id,
      headers: { Authorization: "Bearer " + accessToken },
    }).then();
  };

  // eslint-disable-next-line
  const [SelectedCategories, setSelectedCategories] = useState([]);
  const [filteredFiles, setFilteredFiles] = useState([]);
  const EditSelectedCategories = (newItem) => {
    if (SelectedCategories.indexOf(newItem) === -1) {
      SelectedCategories.push(newItem);
      setFilteredFiles(
        storedFiles.filter((x) => SelectedCategories.includes(x.categoryId))
      );
    } else {
      SelectedCategories.splice(SelectedCategories.indexOf(newItem), 1) &&
        setFilteredFiles(
          storedFiles.filter((x) => SelectedCategories.includes(x.categoryId))
        );
    }
    console.log(SelectedCategories);
  };
  /*function EditSelectedCategories(newItem) {
    let newCategories = SelectedCategories;
    setSelectedCategories([]);
    console.log(SelectedCategories);
    console.log(newItem);
    if (SelectedCategories.indexOf(newItem) === -1) {
      setTimeout(() => {
        Axios.get("/api/Item?category=" + newItem, config).then((res) => {
          console.log(res.data);
          merged = merged.concat(res.data); // merge two arrays
          let foo = new Map();
          for (const tag of merged) {
            foo.set(tag.id, tag);
          }
          newCategories.push(newItem);
          setFilteredFiles([...foo.values()]);
        });
      }, 500);
    } else {
      setTimeout(() => {
        setFilteredFiles(
          filteredFiles.filter((x) => {
            return x.categoryId !== newItem;
          })
        );
        newCategories = newCategories.splice(newCategories.indexOf(newItem), 1);
      }, 500);
    }
    setSelectedCategories(newCategories);
    console.log(SelectedCategories);
  }*/

  useEffect(() => {}, [SelectedCategories]);

  const filterFunction = (array) => {
    if (filteredFiles.length > 0) {
      setFilteredFiles(filteredFiles.reverse());
    }
    setIsFiltered(!isFiltered);
    setStoredFiles(array.reverse());
  };

  const [inputText, setInputText] = useState("");
  let inputHandler = (e) => {
    //convert input text to lower case
    var lowerCase = e.target.value.toLowerCase();
    setInputText(lowerCase);
  };

  useEffect(() => {
    const fetchCategories = async () => {
      const { data } = await Axios.get("/api/Item/Category", config);
      updateCategories(data);
    };
    fetchCategories();

    const fetchDeletedFiles = async () => {
      const { data } = await Axios.get("/api/Item/Deleted", config);
      setDeletedFiles(data);
    };
    fetchDeletedFiles();
    // eslint-disable-next-line
  }, [accessToken]);

  setTimeout(() => {
    ReactDOM.render(
      <StyledDiv>
        <StyledFilterButton
          isFiltered={isFiltered}
          onClick={() => filterFunction(storedFiles, filteredFiles)}
        >
          <i className="fas fa-sort-amount-up-alt"></i>
        </StyledFilterButton>
      </StyledDiv>,
      document.getElementById("filter")
    );
    ReactDOM.render(
      <StyledSearchBox>
        <StyledSearchBoxWithin>
          <input
            id="searchField"
            type="text"
            placeholder="Hledat..."
            onChange={inputHandler}
          />
          <span>
            <i className="fas fa-search"></i>
          </span>
        </StyledSearchBoxWithin>
      </StyledSearchBox>,
      document.getElementById("searchme")
    );
    ReactDOM.render(
      <Button
        text="Přidat"
        type="green"
        onClick={() => history.push("/admin/list/add")}
      />,
      document.getElementById("buttonme")
    );
  }, 250);

  const ApiReturned = () => {
    if (status === "success") {
      setStoredFiles(data);

      return (
        <>
          {!filteredFiles.length <= 0 ? (
            <StyledContentGrid>
              {filteredFiles
                .filter(
                  (obj) =>
                    obj.name.toLowerCase().indexOf(inputText.toLowerCase()) >= 0
                )
                .map((i, index) => {
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
                          onClick={() => handleClick(i.id)}
                        >
                          <i className="fas fa-pen"></i>
                        </Badge>
                      </Card>
                    </>
                  );
                })}
            </StyledContentGrid>
          ) : (
            <StyledContentGrid>
              <>
                {storedFiles
                  .filter(
                    (obj) =>
                      obj.name.toLowerCase().indexOf(inputText.toLowerCase()) >=
                      0
                  )
                  .map((i) => {
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
                            onClick={() => handleClick(i.id)}
                          >
                            <i className="fas fa-pen"></i>
                          </Badge>
                        </Card>
                      </>
                    );
                  })}
                {deletedFiles
                  .filter(
                    (obj) =>
                      obj.name.toLowerCase().indexOf(inputText.toLowerCase()) >=
                      0
                  )
                  .map((i) => {
                    return (
                      <>
                        <Card key={i} className="proomka-card deleted">
                          <CardImage src={i.id}></CardImage>
                          <p className="card-header">{i.name}</p>
                          <p className="card-desc">{i.description}</p>
                          <Badge
                            color="#d05555"
                            colorHover="#a84646"
                            textColor="white"
                            onClick={() => handleDelete(i.id)}
                          >
                            <i className="fas fa-hand-holding-heart"></i>
                          </Badge>
                        </Card>
                      </>
                    );
                  })}
              </>
            </StyledContentGrid>
          )}
        </>
      );
    }

    if (status === "loading") {
      return (
        <StyledContentGrid>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
          <Card>
            <div className="loading-img"></div>
            <p className="card-header loading"></p>
            <p className="card-desc loading"></p>
            <Badge
              className="loading proomka-badge"
              color="#007784"
              colorHover="#009fb1"
              textColor="white"
            >
              <i className="fas fa-cart-plus"></i>
            </Badge>
          </Card>
        </StyledContentGrid>
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

    return <div></div>;
  };

  return (
    <StyledMainGrid>
      <Switch>
        <Route path="/admin/list/add">
          <AdminAdd />
        </Route>
        <Route path="/admin/list/:id">
          <AdminEditItems />
        </Route>
      </Switch>
      <StyledIntroImage bgImage={wave}>
        <h1>Domovská stránka</h1>
      </StyledIntroImage>
      <ContentMenu isAdmin></ContentMenu>
      <div className="shadow-wrap">
        <StyledcategoryWrapper>
          <div className="shadow"></div>
          <div className="shadow"></div>
          {Categrories.map((category, i) => (
            <div
              key={i}
              onClick={() => {
                EditSelectedCategories(category.id);
              }}
            >
              <CategoryButton key={i} onClick={() => console.log("AHOOJ")}>
                <p>{category.name}</p>
              </CategoryButton>
            </div>
          ))}
        </StyledcategoryWrapper>
      </div>
      <ApiReturned />
    </StyledMainGrid>
  );
};

export default AdminItems;
