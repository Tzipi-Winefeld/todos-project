//import axios from 'axios';
import axios from './axiosConfig.ts';

axios.defaults.baseURL = process.env.REACT_APP_VARIABLE_NAME
// "https://localhost:7192"

export default {
  getTasks: async () => {
    const result = await axios.get(`/`) 
       
    return result.data;
  },

  addTask: async(name)=>{
    const result = await axios.post(`/${name}`)    

    console.log('addTask', name)
    //TODO
    return {};
  },

  setCompleted: async(id, isComplete)=>{
    const result = await axios.put(`/${id}/${isComplete}`)
    console.log('setCompleted', {id, isComplete})
    //TODO
    return {};
  },

  deleteTask:async(id)=>{
    const result = await axios.delete(`/${id}`)
    console.log('deleteTask')
  }
};
