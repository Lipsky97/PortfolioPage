import React, { Fragment, useState } from 'react';
import axios from 'axios';

function Registration() {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleNameChange = (value) => {
        setName(value);
    }
    const handleEmailChange = (value) => {
        setEmail(value);
    }
    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    const handleSave = () => {
        const data = {
            UserName: name,
            Email: email,
            Password: password
        };
        const url = 'https://localhost:44314/api/Login/Register';
        axios.post(url, data).then((result) => {
            if (result.data == 'Success')
                alert('You can now sign in');
            else
                alert(result.data);
        }).catch((error) => {
            alert(error);
        })
    }

    return (
        <Fragment>
            <label>Username: </label>
            <input type="text" id="username" placeholder="Username" onChange={(e) => handleNameChange(e.target.value)} />
            <input type="text" id="email" placeholder="example@example.com" onChange={(e) => handleEmailChange(e.target.value)} />
            <input type="password" id="password" placeholder="Password" onChange={(e) => handlePasswordChange(e.target.value)} />
            <button onClick={() => handleSave}>Register</button>
        </Fragment>
    );
}

export default Registration;