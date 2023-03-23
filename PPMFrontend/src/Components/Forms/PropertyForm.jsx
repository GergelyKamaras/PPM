import { useAuth } from "../../Contexts/AuthContext";
import SendData from "../../DataCommunication/SendData";

export function PropertyForm({type, url}) {
    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

    const handleSubmit = (e) =>
    {
        e.preventDefault();
        let form = e.target.form;
        console.log(form);
        let payload = JSON.stringify({
            name : form[1].value,
            size : form[2].value,
            purchasePrice: form[3].value,
            purchaseDate: form[4].value,
            address: {
                country: form[5].value,
                city: form[6].value,
                zipCode: form[7].value,
                street: form[8].value,
                streetNumber: form[9].value,
                additionalInfo: form[10].value
            },
            isRental: form[0].checked,
            ownerId: form[11].value
        });
        console.log(payload);
        SendData("POST", url, payload);
    }

        return (
        <form>
            <div className="form-check">
                <input type="checkbox" className="form-check-input" id="isRental"></input>
                <label className="form-check-label" htmlFor="isRental">Is the property for rent?</label>
            </div>
            <div className="form-group">
                <label htmlFor="Name">Name</label>
                <input type="text" className="form-control" id="Name" placeholder="Enter Property Name" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="Size">Property Size</label>
                <input type="number" className="form-control" id="Size" placeholder="Property size in m2" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="Price">Purchase price</label>
                <input type="number" className="form-control" id="Price" placeholder="Property Price in HUF" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="Date">Purchase Date</label>
                <input type="date" className="form-control" id="Date" required></input>
            </div>
            <div className="form-group">
                <p>Address</p>
                <label htmlFor="Country">Country</label>
                <input type="text" className="form-control" id="Country" placeholder="Enter Country Name" required></input>
                <label htmlFor="City">City</label>
                <input type="text" className="form-control" id="City" placeholder="Enter City Name" required></input>
                <label htmlFor="ZipCode">ZipCode</label>
                <input type="text" className="form-control" id="ZipCode" placeholder="Enter ZipCode" required></input>
                <label htmlFor="Street">Street</label>
                <input type="text" className="form-control" id="Street" placeholder="Enter Street Name" required></input>
                <label htmlFor="StreetNumber">Street Number</label>
                <input type="number" className="form-control" id="StreetNumber" placeholder="Enter Street Number" required></input>
                <label htmlFor="AdditionalInfo">Additional Information</label>
                <input type="text" className="form-control" id="AdditionalInfo" placeholder="Additional information regardin the address"></input>
            </div>
            <input type="hidden" id="ownerId" value={authUser["Id"]}></input>
            <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
        </form>
    )
}
