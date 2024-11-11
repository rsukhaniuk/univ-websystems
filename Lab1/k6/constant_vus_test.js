import http from 'k6/http';
import { check,sleep } from 'k6';

export const options = {
  discardResponseBodies: true,
  scenarios: {
      constant_load: {
          executor: 'constant-vus',
          vus: 10,
          duration: '30s',
      },
  },
  cloud: {
      projectID: 3723689,
      name: 'Constant VUs'
  }
};

export default function () {
  const productId = getRandomProductId(1, 6); 
  const url = `http://localhost:5234/products/${productId}`;
  //const url = `http://lab1:8080/products/${productId}`;

  const res = http.get(url);

  check(res, {
    'Response status is 200': (r) => r.status === 200,
  });

    sleep(generateRandomDelay(0.5, 2));
}

function getRandomProductId(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

function generateRandomDelay(min, max) {
  return Math.random() * (max - min) + min;
}


