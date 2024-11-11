import http from 'k6/http';
import { check,sleep } from 'k6';

export const options = {
  discardResponseBodies: true,
  scenarios: {
      ramping_load: {
          executor: 'ramping-vus',
          startVUs: 0,
          stages: [
            { duration: '60s', target: 20 },
            { duration: '60s', target: 20 },
            { duration: '60s', target: 0 },
          ],
          gracefulRampDown: '0s',
    },
},
  cloud: {
      projectID: 3723622,
      name: 'Ramping VUs'
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


