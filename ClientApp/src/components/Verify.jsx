import React, {useEffect} from 'react';
import { useParams } from 'react-router-dom';
function Verify({uri}) {
    const hasFetchedData = React.useRef(false);
    const params = useParams();
    const [message,setMessage] = React.useState("VERIFYING PHONE NUMBER");

    useEffect(() => {
        if (!hasFetchedData.current){
            fetch(uri+"/verify-subscriber/" + params.id,{
                mode: 'no-cors',
                method: 'POST'
            }).then(res => {
                return res.json()
            }).then(data => {
                setMessage(data.message);
            }).catch(e => {
                console.log(e);
                setMessage(e.message);
            });

            hasFetchedData.current = true;
        } 
    }, [uri, params]);

    return (<h1 className='text-center'>{message}</h1>);
}

export default Verify;