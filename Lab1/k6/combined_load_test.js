import http from 'k6/http';
import { check, sleep } from 'k6';

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
    constant_load: {
      executor: 'constant-vus',
      vus: 10,
      duration: '30s',
      startTime: '35s', // Запускається після завершення constant_rate
    },
    ramping_load: {
      executor: 'ramping-vus',
      startVUs: 0,
      stages: [
        { duration: '60s', target: 20 },
        { duration: '60s', target: 20 },
        { duration: '60s', target: 0 },
      ],
      gracefulRampDown: '0s',
      startTime: '70s', // Запускається після завершення constant_load
    },
  },
  cloud: {
    projectID: 3723622,
    name: 'Combined Load Test',
  },
};

export default function () {
  const productId = getRandomProductId(1, 6);
    const url = `http://lab1:8080/products/${productId}`;
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