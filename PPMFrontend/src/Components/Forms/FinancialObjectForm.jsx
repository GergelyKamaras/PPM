import { useAuth } from "../../Contexts/AuthContext";
import SendData from "../../DataCommunication/SendData";
import { useState, useEffect } from "react";
import FetchData from "../../DataCommunication/FetchData";
import { getAllPropertiesByOwner } from "../../Config";

export function FinancialObjectForm({url, handleClose}) {
    const [isRental, setRental] = useState(false);

    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();
    /*
    useEffect(async () => {
        console.log("And I run");
        let userId = authUser["Id"];
        async function populateProperties() {
            let properties = await FetchData(getAllPropertiesByOwner + authUser["Id"]);
            console.log({properties});
            https://stackoverflow.com/questions/72301355/how-to-populate-select-options-from-an-api-call-in-react-js-on-page-load

        }
        populateProperties();
    }, []);
    */

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
        //handleClose();
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
                    <option value="">--Type of financiaL object--</option>
                    <option value="Cost">Cost</option>
                    <option value="Revenue">Revenue</option>
                    <option value="ValueIncrease">Value Increase</option>
                    <option value="ValueDecrease">Value Decrease</option>
                </select>
            </div>
            <div className="form-group">
                <label htmlFor="propertyId">Property</label>
                <select id="propertyId" required>
                    <option value="">--Please choose related property--</option>
                </select>
            </div>
            <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
        </form>
    )
}
