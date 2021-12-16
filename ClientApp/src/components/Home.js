import React, {useState, useReducer} from 'react';

const formReducer = (state, action) => {
  return {
    ...state,
    [action.name]:action.value
  }
}

function Home({uri}){
  const [submitData,setSubmitData] = useReducer(formReducer,{
    PhoneNumber:"",
    CountryCode:""
  });

  const [response,setResponse] = useState({
    message: "",
    complete: false,
    error: false
  });
  
  const handleChange = e => {
    setSubmitData(e.target);
  }

  function parsePhoneNumber(unverifiedPhoneNumber){
    return (unverifiedPhoneNumber.match(/\d/g) || []).length === 10
  }

  const handleSubmit = e => {
    e.preventDefault();
    
    let parsedNumber = parsePhoneNumber(submitData.PhoneNumber);
    console.log(parsedNumber);
    if(!parsedNumber){
      setResponse({
        error: true,
        message:"Phone number is invalid",
        complete: true
      })
      return;
    }

    let formData = new FormData();
    formData.append("PhoneNumber",submitData.PhoneNumber);
    formData.append("CountryCode",submitData.CountryCode);

    fetch(uri+"/unverified-subscriber",{
      mode: 'no-cors',
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: formData
    }).then(res => {
      return res.json()
    }).then(data => {
      setResponse({
        error: data.error,
        message:data.message,
        complete: true
      })
    }).catch(e => {
      console.log(e);
      setResponse({
        message: "Unable to send message to entered number",
        error: true,
        complete: true
      })
    });
  }

  return(
    <>
      <h1 className='text-center mb-5'>Register To Recieve A Joke Every Hour!</h1>
      <form onSubmit={handleSubmit} className='col col-md-4'>
        <div className='form-group mb-3'>
          <input type="number" className='form-control form-control-lg' name='CountryCode' onChange={handleChange} placeholder='Country Code' required></input>
        </div>
        <div className='form-group mb-3'>
          <input type="tel" className='form-control form-control-lg' name='PhoneNumber' onChange={handleChange} placeholder='Phone Number eg:1234567890' required></input>
        </div>
        <button type='submit' className='btn btn-primary btn-lg btn-block col-12'>Register</button>
      </form>
      {response.complete && <p className={'verification-msg text-center mt-5 ' + (response.error ? 'error' : '')}>{response.message}</p>}
    </>
  )
}

export default Home;
