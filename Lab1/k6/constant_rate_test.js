import http from 'k6/http';
import { check,sleep } from 'k6';

export const options = {
  discardResponseBodies: true,
  scenarios: {
      constant_rate: {
          executor: 'constant-arrival-rate',
          duration: '30s',        
          rate: 25,               
          timeUnit: '1s',
          preAllocatedVUs: 5,     
          maxVUs: 35,  
  },
},
  cloud: {
      projectID: 3723622,
      name: 'Constant Rate'
  }
};

export default function () {
  const productId = getRandomProductId(1, 6); 
  const url = `http://localhost:5234/products/${productId}`;
  const res = http.get(url);

  check(res, {
    'Response status is 200': (r) => r.status === 200,
  });

  sleep(generateRandomDelay(1, 5)); 
}

function getRandomProductId(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

function generateRandomDelay(min, max) {
  return Math.random() * (max - min) + min;
}


