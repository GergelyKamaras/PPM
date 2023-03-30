import { useAuth } from "../../Contexts/AuthContext";
import SendData from "../../DataCommunication/SendData";
import { useState, useEffect } from "react";
import FetchData from "../../DataCommunication/FetchData";
import { getAllPropertiesByOwnerEndpoint } from "../../Config";

export function FinancialObjectForm({url, handleClose}) {
    const [properties, setProperties] = useState([]);

    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

    useEffect(() => {
        // Via https://stackoverflow.com/questions/72301355/how-to-populate-select-options-from-an-api-call-in-react-js-on-page-load
        async function getProperties() {
            const data = await FetchData(getAllPropertiesByOwnerEndpoint + authUser["Id"]);
            const results = [];

            data.forEach(element => {
                results.push({
                    name : element["name"],
                    key : element["id"]
                });
            });

            setProperties([
                {name: 'Select a property', key: ''},
                ...results
            ]);
        }

        getProperties();

    }, []);


    const handleSubmit = (e) =>
    {
        e.preventDefault();
        let form = e.target.form;
        console.log({form});
        let payload = JSON.stringify({
            title : form.querySelector("#title").value,
            description : form.querySelector("#description").value,
            value: form.querySelector("#value").value,
            date: form.querySelector("#date").value,
            financialObjectType: form.querySelector("#financialObjectType").value,
            propertyId: form.querySelector("#propertyId").value
        });
        console.log(payload);
        SendData("POST", url, payload);
        handleClose();
        alert("Form submitted!");
    };

    return (
        <form>
            <h3>Basic information</h3>
            <div className="form-group">
                <label htmlFor="title">Title</label>
                <input type="text" className="form-control" id="title" placeholder="Enter Financial Object Title" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="description">Description</label>
                <input type="text" className="form-control" id="description" placeholder="Enter Financial Object Description" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="value">Value</label>
                <input type="number" className="form-control" id="value" placeholder="Financial Object value in HUF" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="date">Date</label>
                <input type="date" className="form-control" id="date" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="financialObjectType">Type</label>
                <select id="financialObjectType" required>
                    <option value="">--Type of financial object--</option>
                    <option value="Cost">Cost</option>
                    <option value="Revenue">Revenue</option>
                    <option value="ValueIncrease">Value Increase</option>
                    <option value="ValueDecrease">Value Decrease</option>
                </select>
            </div>
            <div className="form-group">
                <label htmlFor="propertyId">Property</label>
                <select id="propertyId" required>
                    {properties.map((property) => {
                        return (
                            <option value={property.key}>{property.name}</option>
                        )
                    })}
                </select>
            </div>
            <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
        </form>
    )
}
