// Imports
// import { kebabCase } from 'lodash'


//execute this instead of configuring routes object manually and fall into import hell
export function route (name, component, path = '', module) {
    component = Object(component) === component
      ? component
      : { default: name.replace(' ', '').toLowerCase() }
  
    const components = {}
  
    for (const [key, value] of Object.entries(component)) {
      components[key] = () => import(
        /* webpackChunkName: "views-[request]" */
        `../components/views/${module}/${value}`
      )
    }
  
    return {
      name,
      components,
      path,
    }
  }