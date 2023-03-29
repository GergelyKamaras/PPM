import { useAuth } from "../../Contexts/AuthContext";
import SendData from "../../DataCommunication/SendData";
import { useState } from "react";

export function PropertyForm({url, handleClose}) {
    const [isRental, setRental] = useState(false);

    const {authUser,
        setAuthUser,
        isLoggedIn,
        setIsLoggedIn} = useAuth();

        const handleSubmit = (e) =>
    {
        e.preventDefault();
        let form = e.target.form;
        let payload = JSON.stringify({
            name : form.querySelector("#name").value,
            size : form.querySelector("#size").value,
            purchasePrice: form.querySelector("#purchasePrice").value,
            purchaseDate: form.querySelector("#purchaseDate").value,
            address: {
                country: form.querySelector("#country").value,
                city: form.querySelector("#city").value,
                zipCode: form.querySelector("#zipCode").value,
                street: form.querySelector("#street").value,
                streetNumber: form.querySelector("#streetNumber").value,
                additionalInfo: form.querySelector("#additionalInfo").value
            },
            isRental: form.querySelector("#isRental").checked,
            ownerId: form.querySelector("#ownerId").value,
            rentalFee: isRental ? form.querySelector("#rentalPrice").value : 0
        });
        SendData("POST", url, payload);
        handleClose();
    };

    return (
        <form>
            <h3>Basic information</h3>
            <div className="form-check">
                <label className="form-check-label" htmlFor="isRental">Is the property for rent?</label>
                <input type="checkbox" className="form-check-input" id="isRental" onChange={() => isRental ? setRental(false) : setRental(true)}></input>
            </div>
            <div className="form-group">
                <label htmlFor="name">Name</label>
                <input type="text" className="form-control" id="name" placeholder="Enter Property Name" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="size">Property Size</label>
                <input type="number" className="form-control" id="size" placeholder="Property size in m2" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="purchasePrice">Purchase price</label>
                <input type="number" className="form-control" id="purchasePrice" placeholder="Property Price in HUF" required></input>
            </div>
            <div className="form-group">
                <label htmlFor="purchaseDate">Purchase Date</label>
                <input type="date" className="form-control" id="purchaseDate" required></input>
            </div>
            <div className="form-group">
                <h3>Address</h3>
                <label htmlFor="country">Country</label>
                <input type="text" className="form-control" id="country" placeholder="Enter Country Name" required></input>
                <label htmlFor="city">City</label>
                <input type="text" className="form-control" id="city" placeholder="Enter City Name" required></input>
                <label htmlFor="zipCode">ZipCode</label>
                <input type="text" className="form-control" id="zipCode" placeholder="Enter ZipCode" required></input>
                <label htmlFor="street">Street</label>
                <input type="text" className="form-control" id="street" placeholder="Enter Street Name" required></input>
                <label htmlFor="streetNumber">Street Number</label>
                <input type="number" className="form-control" id="streetNumber" placeholder="Enter Street Number" required></input>
                <label htmlFor="additionalInfo">Additional Information</label>
                <input type="text" className="form-control" id="additionalInfo" placeholder="Additional information regardin the address"></input>
            </div>
            <input type="hidden" id="ownerId" value={authUser["Id"]}></input>
            {isRental &&
            <div className="form-group">
                <h3>Rental information</h3>
                <label htmlFor="rentalPrice">Rental price</label>
                <input type="number" className="form-control" id="rentalPrice" placeholder="Rental Price in HUF" required></input>
                <p>Placeholder for tenant selector</p>
            </div>
            }
            <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
        </form>
    )
}
