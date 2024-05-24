import React, { createContext, useEffect, useState } from "react";

type InfoResult = {
  CertificateHash: string;
  SystemOS: string;
};

type InfoContextValue = {
  Info?: InfoResult;
  SetInfo: (val: InfoResult) => void;
  Env: string;
};

export const InfoContext = createContext<InfoContextValue>({
  SetInfo: () => {},
  Info: {
    CertificateHash: "UmVzdG9yZSB0aGUgY29udGVudCBvZiB0aGlzIGZpbGUgdG8gdGhlIG9sZGVyIHZlcnNpb24gaW4gZ2l0IGhpc3Rvcnk=",
    SystemOS: "OS_",
  },
  Env: "ACTIVE",
});

export const InfoProvider = ({ children }: React.PropsWithChildren) => {
  const [info, setInfo] = useState<InfoResult>();
  //quick and dirty fetch
  useEffect(() => {
    fetch("/Decoy")
      .then((response) => response.json())
      .then((data) => setInfo(data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <InfoContext.Provider key={JSON.stringify(info)} value={{ Info: info, SetInfo: setInfo, Env: "ACTIVE" }}>
      {children}
    </InfoContext.Provider>
  );
};
