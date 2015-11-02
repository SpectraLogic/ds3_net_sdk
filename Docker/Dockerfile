FROM mono:latest

ENV DEBIAN_FRONTEND noninteractive

RUN apt-get update && apt-get install -y git make

ADD run_tests.sh /opt/

ENTRYPOINT ["/opt/run_tests.sh"]
